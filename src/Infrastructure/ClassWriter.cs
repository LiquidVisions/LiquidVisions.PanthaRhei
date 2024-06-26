﻿using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;

namespace LiquidVisions.PanthaRhei.Infrastructure
{
    /// <summary>
    /// A File writer helper.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ClassWriter"/> class.
    /// </remarks>
    /// <param name="fileService"><seealso cref="IFile"/></param>
    /// <param name="logger"><seealso cref="ILogger"/></param>
    internal class ClassWriter(IFile fileService, ILogger logger) : IWriter
    {
        private List<string> lines;

        /// <summary>
        /// Gets the list of lines representing the file.
        /// </summary>
        public List<string> Lines => lines;

        /// <inheritdoc/>
        public void AddNameSpace(string name)
        {
            string text = $"using {name};";
            int index = IndexOf(text);
            if (index == -1)
            {
                index = lines.IndexOf(lines.LastOrDefault(x => x.Contains("using ", StringComparison.InvariantCulture) && x.EndsWith(';')));

                logger.Trace($"Adding namespace {name} to the file.");

                WriteAt(index, $"using {name};");
            }
        }

        /// <inheritdoc/>
        public void Load(string path)
        {
            logger.Trace($"Reading file {path}");

            lines = [..fileService.ReadAllLines(path)];
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
            while (!string.IsNullOrEmpty(lines[index]))
            {
                index++;
            }

            WriteAt(index, text);
        }

        /// <inheritdoc/>
        public int IndexOf(string match)
        {
            int index = lines.IndexOf(lines.Find(x => x.Contains(match, StringComparison.InvariantCulture)));

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
                lines[index] = lines[index].Replace(match, replaceValue, StringComparison.InvariantCulture);
            }
        }

        public void AddBetween(string beginMatch, string endMatch, string replaceValue)
        {
            int beginIndex = IndexOf(beginMatch);
            int endIndex = IndexOf(endMatch);

            int count = endIndex - 1 - beginIndex;
            lines.RemoveRange(beginIndex + 1, count);

            logger.Trace($"Writing {replaceValue} to file.");

            string line = lines[beginIndex];
            int padCount = line.TakeWhile(char.IsWhiteSpace).Count();
            int total = 0;
            for (int i = 0; i < padCount; i++)
            {
                if (line[i] == '\t')
                {
                    total += 4;
                }
                else
                {
                    total += 1;
                }
            }

            replaceValue = replaceValue.PadLeft(total + replaceValue.Length, ' ');
            lines.Insert(beginIndex + 1, replaceValue);
        }

        /// <inheritdoc/>
        public int LastIndexOf(string match)
        {
            int index = lines.LastIndexOf(lines.LastOrDefault(x => x.Contains(match, StringComparison.InvariantCulture)));

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

                if (str.Contains('{', StringComparison.InvariantCulture))
                {
                    stack.Push(str);
                }

                if (str.Contains('}', StringComparison.InvariantCulture))
                {
                    stack.Pop();
                }
            }

            index--;

            Lines.Insert(index, statement);
        }

        private int IndexOf(string match, int index)
        {
            int result = lines.IndexOf(lines.Find(x => x.Contains(match, StringComparison.InvariantCulture)), index);

            logger.Trace($"Matched index on match '{match}' is {result}");

            return result;
        }
    }
}
