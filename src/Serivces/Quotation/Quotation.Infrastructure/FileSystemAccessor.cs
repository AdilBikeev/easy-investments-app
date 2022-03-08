using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Quotation.BuildingBlocks.Database.Abstractions;

namespace Quotation.Infrastructure
{
    /// <summary>
    /// Класс для работы с файлами данных для БД.
    /// </summary>
    public class FileSystemAccessor : IFileSystemAccessor
    {
        private readonly string? _assemblyLocation;

        public FileSystemAccessor()
        {
            _assemblyLocation = Assembly.GetEntryAssembly()?.Location;
        }

        /// <inheritdoc/>
        public string GetWorkingDirectory(string relativePath)
        {
            if (string.IsNullOrEmpty(_assemblyLocation))
                throw new Exception($"{nameof(FileSystemAccessor)}: Cannot determine assembly location.");
            var executionDir = new FileInfo(_assemblyLocation).Directory?.FullName;
            if (string.IsNullOrEmpty(executionDir) || !Directory.Exists(executionDir))
                throw new Exception($"{nameof(FileSystemAccessor)}: Execution directory {executionDir} doesn't exist.");
            var path = Path.Combine(executionDir, relativePath);
            if (!Directory.Exists(path))
                throw new Exception($"{nameof(FileSystemAccessor)}: Specified directory {path} doesn't exist.");

            return path;
        }
    }
}
