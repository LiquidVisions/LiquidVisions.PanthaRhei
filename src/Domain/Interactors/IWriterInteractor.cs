namespace LiquidVisions.PanthaRhei.Domain.Interactors
{
    /// <summary>
    /// Interface that represents a file writing class.
    /// </summary>
    public interface IWriterInteractor
    {
        /// <summary>
        /// Loads an existing file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        void Load(string path);

        /// <summary>
        /// Saves to a file location.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        void Save(string path);

        /// <summary>
        /// Adds text to the file at the matched position.
        /// </summary>
        /// <param name="match">Match that identifies the position index.</param>
        /// <param name="text">The text that is added to the file.</param>
        /// <returns>the index number of the match.</returns>
        int WriteAt(string match, string text);

        /// <summary>
        /// Adds text to the file at the given position.
        /// </summary>
        /// <param name="index">the index position.</param>
        /// <param name="text">The text that is added to the file.</param>
        void WriteAt(int index, string text);

        /// <summary>
        /// Adds a using to the file.
        /// </summary>
        /// <param name="nameSpace">the namespace.</param>
        void AddNameSpace(string nameSpace);

        /// <summary>
        /// Gets the index of the first matched string.
        /// </summary>
        /// <param name="match">Match that identifies the position index.</param>
        /// <returns><seealso cref="int"/></returns>
        int IndexOf(string match);

        /// <summary>
        /// Gets the index of the last matched string.
        /// </summary>
        /// <param name="match">Match that identifies the position index.</param>
        /// <returns><seealso cref="int"/></returns>
        int LastIndexOf(string match);

        /// <summary>
        /// Removes a block of lines between the match and the matchUntil paramaters.
        /// </summary>
        /// <param name="match">The starting match.</param>
        /// <param name="matchUntil">The unil match.</param>
        void RemoveLinesUntil(string match, string matchUntil);

        /// <summary>
        /// Replaces the matched string with the replace value parameter.
        /// </summary>
        /// <param name="match">The match string.</param>
        /// <param name="replaceValue">The replace value.</param>
        void Replace(string match, string replaceValue);

        /// <summary>
        /// Adds a string value between 2 anchors.
        /// </summary>
        /// <param name="beginMatch">start of the match.</param>
        /// <param name="endMatch">end of the match.</param>
        /// <param name="replaceValue">value to add between beginMatch and endMatch.</param>
        void AddBetween(string beginMatch, string endMatch, string replaceValue);

        /// <summary>
        /// Adds text on the first empty row after the last match.
        /// </summary>
        /// <param name="match">The match string.</param>
        /// <param name="text">The text that will be inserted.</param>
        void WriteAtEmptyRow(string match, string text);

        /// <summary>
        /// Appends the text at the bottom.
        /// </summary>
        /// <param name="text">the text that will be appended.</param>
        void AppendMethodToClass(string text);

        /// <summary>
        /// Replaces an existing method in the given class. It is assumed that the first line of the <paramref name="text"/> identifies the method name.
        /// </summary>
        /// <param name="text">The replace value of the method.</param>
        void AddOrReplaceMethod(string text);

        /// <summary>
        /// Adds a statement to a method.
        /// </summary>
        /// <param name="method">The method where the statement is appended.</param>
        /// <param name="statement">The statement.</param>
        void AppendToMethod(string method, string statement);
    }
}
