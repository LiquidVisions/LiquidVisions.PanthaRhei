using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure
{
    /// <summary>
    /// A File writer helper.
    /// </summary>
    internal class ClassWriter : IWriter
    {
        private readonly IFile fileService;
        private readonly ILogger logger;
        private List<string> lines;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWriter"/> class.
        /// </summary>
        /// <param name="fileService"><seealso cref="IFile"/></param>
        /// <param name="logger"><seealso cref="ILogger"/></param>
        public ClassWriter(IFile fileService, ILogger logger)
        {
            this.fileService = fileService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the list of lines representing the file.
        /// </summary>
        public List<string> Lines => lines;

        /// <inheritdoc/>
        public void AddNameSpace(string nameSpace)
        {
            string text = $"using {nameSpace};";
            int index = IndexOf(text);
            if (index == -1)
            {
                index = lines.IndexOf(lines.LastOrDefault(x => x.Contains("using ") && x.EndsWith(";")));

                logger.Trace($"Adding namespace {nameSpace} to the file.");

                WriteAt(index, $"using {nameSpace};");
            }
        }

        /// <inheritdoc/>
        public void Load(string path)
        {
            logger.Trace($"Reading file {path}");

            lines = fileService
                .ReadAllLines(path)
                .ToList();
        }

        /// <inheritdoc/>
        public void Save(string path)
        {
            logger.Trace($"Saving file {path}.");

            fileService.WriteAllLines(path, lines);
            lines = null;
        }

        /// <inheritdoc/>
        public int WriteAt(string match, string text)
        {
            int index = IndexOf(match);
            if (index > -1)
            {
                logger.Trace($"Writing {text} to file.");
                WriteAt(index, text);
            }

            return index;
        }

        /// <inheritdoc/>
        public void WriteAt(int index, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                if (!string.IsNullOrEmpty(lines[index - 1]))
                {
                    lines.Insert(index, string.Empty);
                }

                return;
            }

            int textIndex = IndexOf(text);
            if (textIndex < 0 && textIndex != index)
            {
                int count = lines[index].TakeWhile(char.IsWhiteSpace).Count();
                text = text.PadLeft(count + text.Length, ' ');
                lines.Insert(index, text);
            }
        }

        /// <inheritdoc/>
        public void WriteAtEmptyRow(string match, string text)
        {
            int index = LastIndexOf(match);
            while (lines[index] != string.Empty)
            {
                index++;
            }

            WriteAt(index, text);
        }

        /// <inheritdoc/>
        public int IndexOf(string match)
        {
            int index = lines.IndexOf(lines.FirstOrDefault(x => x.Contains(match)));

            logger.Trace($"Matched index on match '{match}' is {index}");

            return index;
        }

        /// <inheritdoc/>
        public void RemoveLinesUntil(string match, string matchUntil)
        {
            int index = IndexOf(match);
            if (index > 0)
            {
                int until = IndexOf(matchUntil, index) - index;
                ++until;
                lines.RemoveRange(index, until);
            }
        }

        /// <inheritdoc/>
        public void Replace(string match, string replaceValue)
        {
            // only do the replacement if the replacement value does not exist in the file.
            int indexReplaceValue = IndexOf(replaceValue);
            if (indexReplaceValue > 0)
            {
                return;
            }

            int index = IndexOf(match);
            if (index > -1)
            {
                lines[index] = lines[index].Replace(match, replaceValue);
            }
        }

        public void AddBetween(string beginMatch, string endMatch, string replaceValue)
        {
            int beginIndex = IndexOf(beginMatch);
            int endIndex = IndexOf(endMatch);

            int count = endIndex - 1 - beginIndex;

            lines.RemoveRange(beginIndex + 1, count);

            logger.Trace($"Writing {replaceValue} to file.");
            int padCount = lines[beginIndex].TakeWhile(char.IsWhiteSpace).Count();
            replaceValue = replaceValue.PadLeft(padCount + replaceValue.Length, ' ');
            lines.Insert(beginIndex + 1, replaceValue);
        }

        /// <inheritdoc/>
        public int LastIndexOf(string match)
        {
            int index = lines.LastIndexOf(lines.LastOrDefault(x => x.Contains(match)));

            logger.Trace($"Matched index on match '{match}' is {index}");

            return index;
        }

        /// <inheritdoc/>
        public void AddOrReplaceMethod(string text)
        {
            string[] textArray = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int index = IndexOf(textArray[0]);
            if (index > 0)
            {
                int until = 1;
                int i = index;
                while (lines[i].Trim() != "}")
                {
                    i++;
                    until++;
                }

                lines.RemoveRange(index, until);
            }

            AppendMethodToClass(text);
        }

        /// <summary>
        /// Appends the text at the end of the class.
        /// </summary>
        /// <param name="text">the text that will be append.</param>
        public void AppendMethodToClass(string text)
        {
            lines.Insert(lines.Count - 2, text);
        }

        /// <summary>
        /// Appends the given statement to the bottom of the given method.
        /// </summary>
        /// <param name="method">a <see cref="string"/> that matches the method signature.</param>
        /// <param name="statement">a <seealso cref="string"/> that represents the given statement.</param>
        /// <exception cref="InvalidOperationException"><seealso cref="InvalidOperationException"/>.</exception>
        public void AppendToMethod(string method, string statement)
        {
            int index = IndexOf(method);
            Stack<string> stack = new();

            index++;
            string str = lines[index].Trim();
            if (str != "{")
            {
                throw new InvalidOperationException("Bracket open expected.");
            }

            stack.Push(str);

            while (stack.Count != 0)
            {
                index++;
                str = lines[index].Trim();

                if (str.Contains('{'))
                {
                    stack.Push(str);
                }

                if (str.Contains('}'))
                {
                    stack.Pop();
                }
            }

            index--;

            Lines.Insert(index, statement);
        }

        private int IndexOf(string match, int index)
        {
            int result = lines.IndexOf(lines.FirstOrDefault(x => x.Contains(match)), index);

            logger.Trace($"Matched index on match '{match}' is {result}");

            return result;
        }
    }
}
