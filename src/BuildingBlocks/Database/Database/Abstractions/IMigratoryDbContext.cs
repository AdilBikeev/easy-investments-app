namespace Stock.BuildingBlocks.Database.Abstractions
{
    /// <summary>
    /// Интерфейс для описания механизма миграций.
    /// </summary>
    public interface IMigratoryDbContext
    {
        string SchemaName { get; }
        void Migrate();
    }
}