using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure
{
    /// <summary>
    /// Default object that executes cli commands.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CommandLine : ICommandLineInteractor
    {
        private readonly ILogger logger;
        private bool silent = false;
        private bool hasError;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLine"/> class.
        /// </summary>
        /// <param name="logger"><seealso cref="ILogger"/></param>
        public CommandLine(ILogger logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool UseWindow { get; set; } = false;

        /// <inheritdoc/>
        public bool UseShellExecution { get; set; } = false;

        /// <inheritdoc/>
        public List<string> Output { get; private set; }

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
            this.silent = silent;
            hasError = false;

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

            if (hasError)
            {
                throw new InvalidProgramException();
            }
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Output.Add(e.Data);
                if (!silent)
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
                hasError = true;
            }
        }
    }
}
