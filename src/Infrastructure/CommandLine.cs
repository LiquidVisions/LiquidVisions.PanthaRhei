﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;

namespace LiquidVisions.PanthaRhei.Infrastructure
{
    /// <summary>
    /// Default object that executes cli commands.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CommandLine"/> class.
    /// </remarks>
    /// <param name="logger"><seealso cref="ILogger"/></param>
    [ExcludeFromCodeCoverage]
    public class CommandLine(ILogger logger) : ICommandLine
    {
        private bool _silent;
        private bool _hasError;

        /// <inheritdoc/>
        public bool UseWindow { get; set; }

        /// <inheritdoc/>
        public bool UseShellExecution { get; set; }

        /// <inheritdoc/>
        public ICollection<string> Output { get; private set; }

        /// <inheritdoc/>
        public void Start(string command)
        {
            Start(command, string.Empty, false);
        }

        /// <inheritdoc/>
        public void Start(string command, string workingDirectory)
        {
            Start(command, workingDirectory, false);
        }

        /// <inheritdoc/>
        public void Start(string command, bool silent)
        {
            Start(command, string.Empty, silent);
        }

        /// <inheritdoc/>
        public void Start(string command, string workingDirectory, bool silent)
        {
            _silent = silent;
            _hasError = false;

            logger.Debug($"Executing command '{command}'");
            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                logger.Debug($"Working directory '{workingDirectory}'");
            }

            Output = new List<string>();
            Process process = new()
            {
                StartInfo = new("cmd.exe", $"/C {command}")
                {
                    CreateNoWindow = UseWindow,
                    UseShellExecute = UseShellExecution,
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            process.Dispose();

            if (_hasError)
            {
                throw new InvalidProgramException();
            }
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Output.Add(e.Data);
                if (!_silent)
                {
                    logger.Debug(e.Data);
                }
            }
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                logger.Fatal(e.Data);
                _hasError = true;
            }
        }
    }
}
