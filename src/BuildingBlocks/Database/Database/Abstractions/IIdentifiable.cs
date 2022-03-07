namespace Stock.BuildingBlocks.Database.Abstractions
{
    /// <summary>
    /// Интерфейс для кастомизации Id для БД.
    /// </summary>
    /// <typeparam name="T">Тип данных для идентификатора.</typeparam>
    public interface IIdentifiable<T>
    {
        T Id { get; }
    }
}