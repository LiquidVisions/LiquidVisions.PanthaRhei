using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.IO
{
    /// <summary>
    /// Mockable representation of functionality that are part of <see cref="System.IO.File"/> and <see cref="System.IO.FileInfo"/>.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Gets the fullname of the of the directory of a file.
        /// </summary>
        /// <param name="path">full path to a file.</param>
        /// <returns>Fullname of the directory.</returns>
        string GetDirectory(string path);

        /// <summary>
        /// Checks weather the file exists.
        /// </summary>
        /// <param name="path">Full path to a file.</param>
        /// <returns>a booleaan indication if the file exists or not.</returns>
        bool Exists(string path);

        /// <summary>
        ///  Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The fullpath to the file.</param>
        /// <returns>A string array containing all lines of the file.</returns>
        string[] ReadAllLines(string path);

        /// <summary>
        ///  Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The fullpath to the file.</param>
        /// <returns>A string containing the content of the file.</returns>
        string ReadAllText(string path);

        /// <summary>
        /// Creates a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The fullpath to the file to write to.</param>
        /// <param name="lines">The string collection to write to the file.</param>
        void WriteAllLines(string path, IEnumerable<string> lines);

        /// <summary>
        /// Creates a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The fullpath to the file to write to.</param>
        /// <param name="lines">The string array to write to the file.</param>
        void WriteAllLines(string path, string[] lines);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="text">The string to write to the file.</param>
        void WriteAllText(string path, string text);

        /// <summary>
        /// eturns the file name without the extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to obtain the file name and extension.</param>
        /// <returns>The characters after the last directory separator character in path, without the extension. If the last character of path is a directory or volume separator character, this method returns System.String.Empty. If path is null, this method returns null.</returns>
        string GetFileNameWithoutExtension(string path);
    }
}
