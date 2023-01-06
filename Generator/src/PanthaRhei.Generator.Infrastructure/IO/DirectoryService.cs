using System.Diagnostics.CodeAnalysis;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.IO
{
    /// <summary>
    /// An implementation of <see cref="IDirectory"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class DirectoryService : IDirectory
    {
        /// <inheritdoc/>
        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <inheritdoc/>
        public void Delete(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <inheritdoc/>
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <inheritdoc/>
        public void Rename(string source, string target)
        {
            Directory.Move(source, target);
        }

        /// <inheritdoc/>
        public string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        /// <inheritdoc/>
        public string GetNameOfParentDirectory(string path)
        {
            return new DirectoryInfo(path)
                .Parent
                .FullName;
        }

        /// <inheritdoc/>
        public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        /// <inheritdoc/>
        public string GetDirectyName(string path)
        {
            return new DirectoryInfo(path).Name;
        }

        /// <inheritdoc/>
        public void Copy(string source, string target)
        {
            var directory = new DirectoryInfo(source);
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory not found: {directory.FullName}");
            }

            DirectoryInfo[] directories = directory.GetDirectories();

            Directory.CreateDirectory(target);

            foreach (FileInfo file in directory.GetFiles())
            {
                string targetFilePath = Path.Combine(target, file.Name);
                file.CopyTo(targetFilePath);
            }

            foreach (DirectoryInfo subDir in directories)
            {
                string newDestinationDir = Path.Combine(target, subDir.Name);
                Copy(subDir.FullName, newDestinationDir);
            }
        }
    }
}
