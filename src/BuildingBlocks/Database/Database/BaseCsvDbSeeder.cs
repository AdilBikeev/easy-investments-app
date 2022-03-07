using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Quotation.BuildingBlocks.Database.Abstractions;
using Quotation.BuildingBlocks.Database.Extensions;

namespace Quotation.BuildingBlocks.Database
{
    /// <summary>
    /// Класс представляющая БД в формате CSV.
    /// </summary>
    public abstract class BaseCsvDbSeeder<TDbContext> : IDbSeeder 
        where TDbContext : DbContext, IMigratoryDbContext
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;
        private readonly ILogger<BaseCsvDbSeeder<TDbContext>> _logger;
        private readonly TimeSpan _sqlCommandTimeout = TimeSpan.FromSeconds(60);
        private readonly IFileSystemAccessor _fileAccessor;
        private const string CsvDelimiter = ",";

        protected BaseCsvDbSeeder(
            IDbContextFactory<TDbContext> dbContextFactory,
            ILogger<BaseCsvDbSeeder<TDbContext>> logger,
            IFileSystemAccessor fileAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _fileAccessor = fileAccessor;
        }

        protected string GetTableName<T>(DbContext dbContext) where T : class
        {
            if (dbContext is null) throw new ArgumentNullException(nameof(dbContext));

            var entityType = dbContext.Model.FindEntityType(typeof(T));
            var tableName = entityType.GetTableName();
            if (tableName == null)
                throw new InvalidOperationException($"{nameof(BaseCsvDbSeeder<TDbContext>)}.{nameof(GetTableName)}: Cannot determine table name.");

            return tableName;
        }

        public void InternalSeedData()
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var dbContext = _dbContextFactory.CreateDbContext();

            dbContext.Database.SetCommandTimeout(_sqlCommandTimeout);

            using var connection = dbContext.Database.GetDbConnection();

            SeedData(dbContext);

            scope.Complete();
        }

        public abstract void SeedData(TDbContext dbContext);

        protected void ClearTable<TEntity>(TDbContext dbContext) where TEntity : class
        {
            var tableName = GetTableName<TEntity>(dbContext);
            dbContext.DeleteAllFromTable(tableName, dbContext.SchemaName);
        }

        protected void ImportEntitiesFromCsvFile<TEntity, TEntityMap, TId>(TDbContext dbContext)
            where TEntity : class, IIdentifiable<TId>
            where TEntityMap : ClassMap<TEntity>
        {
            var tableName = GetTableName<TEntity>(dbContext);
            var workingDir = _fileAccessor.GetWorkingDirectory("Data");
            var filePath = Path.Combine(workingDir, $"{tableName}.csv");
            var fi = new FileInfo(filePath);
            _logger.LogInformation($"Importing entities from file {filePath} with size={fi.Length}.");

            var entities = ReadEntitiesFromCsvFile<TEntity, TEntityMap>(filePath);
            var dbEntities = dbContext.Set<TEntity>();
            var entityIds = entities.Select(e => e.Id).ToList();
            var dbEntityIds = dbEntities.Select(e => e.Id).ToList();

            var toDelete = dbEntities.Where(e => !entityIds.Contains(e.Id)).ToList();
            _logger.LogInformation($"Removing records from {tableName}: {toDelete.Count} records found.");

            var toAdd = entities.Where(e => !dbEntityIds.Contains(e.Id)).ToList();
            _logger.LogInformation($"Adding records into {tableName}: {toAdd.Count} records found.");

            var toUpdate = entities.Where(e => dbEntityIds.Contains(e.Id)).ToList();
            _logger.LogInformation($"Updating records in {tableName}: {toUpdate.Count} records found.");

            dbContext.RemoveRange(toDelete);
            dbContext.AddRange(toAdd);
            dbContext.UpdateRange(toUpdate);

            dbContext.SaveChanges();
        }

        protected TEntity[] ReadEntitiesFromCsvFile<TEntity, TEntityMap>(string filePath)
            where TEntity : class
            where TEntityMap : ClassMap<TEntity>
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentException(nameof(filePath));

            using var reader = new StreamReader(filePath);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = CsvDelimiter
                //IncludePrivateMembers = true
            };

            using var csv = new CsvReader(reader, csvConfig);
            try
            {
                csv.Context.RegisterClassMap<TEntityMap>();
                var records = csv.GetRecords<TEntity>().ToArray();
                _logger.LogInformation($"\"{filePath}\" - read {records.Length} records.");

                return records;
            }
            catch (TypeConverterException e)
            {
                var errorStrings = e.Data.Cast<DictionaryEntry>().Select(err => $"{err.Key} : {err.Value}");
                var errors = string.Join(Environment.NewLine, errorStrings);

                _logger.LogError(e, $"\"{filePath}\" - failed to parse the CSV file:{Environment.NewLine}{errors}.");

                throw;
            }
        }
    }
}