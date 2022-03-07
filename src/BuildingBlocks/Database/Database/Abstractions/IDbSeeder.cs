namespace Stock.BuildingBlocks.Database.Abstractions
{
    /// <summary>
    /// Интерфейс для описания методов подгрузки данных для БД.
    /// </summary>
    public interface IDbSeeder
    {
        void InternalSeedData();
    }
}