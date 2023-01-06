namespace LiquidVisions.PanthaRhei.Generator.Domain.IO
{
    /// <summary>
    /// Mockable representation of functionality that are part of <see cref="System.IO.Directory"/> and <see cref="System.IO.DirectoryInfo"/>.
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns>True or false, depending on if the directory exists.</returns>
        bool Exists(string path);

        /// <summary>
        /// Deletes the specified directory and, any subdirectories and files in the directory.
        /// </summary>
        /// <param name="path">The name of the directory to remove.</param>
        void Delete(string path);

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// </summary>
        /// <param name="path">The directory to create.</param>
        void Create(string path);

        /// <summary>
        /// Moves a file or a directory and its contents to a new location.
        /// </summary>
        /// <param name="source">The path of the file or directory to move.</param>
        /// <param name="target">The path to the new directory location.</param>
        void Rename(string source, string target);

        /// <summary>
        /// Retruns the names of the subdirectories that match the search patterns.
        /// </summary>
        /// <param name="path">The search root.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption"><seealso cref="System.IO.SearchOption"/></param>
        /// <returns>An array of subdirecotries.</returns>
        string[] GetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>
        /// Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in path. This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option, or an empty array if no files are found.</returns>
        string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>
        /// Gets the parent name of the directory.
        /// </summary>
        /// <param name="path">Full path to the child directory.</param>
        /// <returns>the name of the parent directory.</returns>
        string GetNameOfParentDirectory(string path);

        /// <summary>
        /// Copies all files and folders to the target destination.
        /// </summary>
        /// <param name="source">The source folder.</param>
        /// <param name="target">the target destination.</param>
        void Copy(string source, string target);

        /// <summary>
        /// Gets the namme of the directory.
        /// </summary>
        /// <param name="path">The full path of the directory.</param>
        /// <returns>The name of the directory.</returns>
        string GetDirectyName(string path);
    }
}
