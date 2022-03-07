namespace Stock.BuildingBlocks.Database.Extensions
{
    public static class DbContextExtensions
    {
        public static void DeleteAllFromTable(this DbContext context, string tableName, string schema)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException(nameof(tableName));

            var schemaPart = string.IsNullOrEmpty(schema) ? "" : $"{schema}.";
            var sql = $"delete from {schemaPart}\"{tableName}\";";
            context.Database.ExecuteSqlRaw(sql);
        }

        /// <summary>
        /// Удаляет все строки из таблицы.
        /// </summary>
        /// <param name="context">Название БД.</param>
        /// <param name="tableName">Название табилцы</param>
        /// <param name="schema">Название схемы.</param>
        public static void TruncateTable(this DbContext context, string tableName, string schema)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException(nameof(tableName));

            var schemaPart = string.IsNullOrEmpty(schema) ? "" : $"{schema}.";
            var sql = $"truncate table {schemaPart}\"{tableName}\";";
            context.Database.ExecuteSqlRaw(sql);
        }
    }
}
