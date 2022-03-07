namespace Quotation.BuildingBlocks.Database.Abstractions
{
    /// <summary>
    /// Интерфейс для работы с файлами для хранения данных.
    /// </summary>
    public interface IFileSystemAccessor
    {
        /// <summary>
        /// Возвращает путь к рабочей дириктории.
        /// </summary>
        /// <param name="relativePath">Относительный путь.</param>
        string GetWorkingDirectory(string relativePath);
    }
}