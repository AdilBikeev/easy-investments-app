using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

namespace Quotation.Infrastructure.Repositories
{
    /// <summary>
    /// Being used to change postgres DB scheme for tables containing EF.Core migrations
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class MigrationHistoryRepository : NpgsqlHistoryRepository
#pragma warning restore EF1001 // Internal EF Core API usage.
    {
        public MigrationHistoryRepository(HistoryRepositoryDependencies dependencies)
#pragma warning disable EF1001 // Internal EF Core API usage.
            : base(dependencies)
#pragma warning restore EF1001 // Internal EF Core API usage.
        {
            // TODO find a more reliable way
        }

        protected override string TableName => "__EFMigrationHistory";

        protected override string TableSchema => QuotationContext.DEFAULT_SCHEMA;
    }
}