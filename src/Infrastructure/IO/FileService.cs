using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Infrastructure.IO
{
    /// <summary>
    /// Implementation of <see cref="IFile"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class FileService : IFile
    {
        /// <inheritdoc/>
        public string GetDirectory(string path)
        {
            return new FileInfo(path)
                .Directory
                .FullName;
        }

        /// <inheritdoc/>
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc/>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <inheritdoc/>
        public void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text, Encoding.UTF8);
        }

        /// <inheritdoc/>
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <inheritdoc/>
        public void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines, Encoding.UTF8);
        }

        /// <inheritdoc/>
        public void WriteAllLines(string path, IEnumerable<string> lines)
            => WriteAllLines(path, lines.ToArray());

        /// <inheritdoc/>
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}
