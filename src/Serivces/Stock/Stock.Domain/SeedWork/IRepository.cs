namespace Stock.Domain.SeedWork
{
    /// <summary>
    /// Общее описание хранилищ данных сушности T.
    /// </summary>
    /// <typeparam name="T">Entity - таблица из БД.</typeparam>
    internal interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }

}
