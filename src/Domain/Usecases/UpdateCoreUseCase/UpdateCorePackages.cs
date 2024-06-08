using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.UpdateCoreUseCase
{
    /// <summary>
    /// Updates the PanthaRhei.Core packages in the csproj files.
    /// </summary>
    /// <param name="directory"><seealso cref="IDirectory"/></param>
    /// <param name="commandLine"><seealso cref="ICommandLine"/></param>
    /// <param name="logger"><seealso cref="ILogger"/></param>
    /// <param name="file"><seealso cref="IFile"/></param>
    public class UpdateCorePackages(IDirectory directory, ICommandLine commandLine, ILogger logger, IFile file) : IUpdateCorePackages
    {
        private readonly Dictionary<string, string> packages = new()
        {
            { "Core", "LiquidVisions.PanthaRhei.Core" },
            { "Tests", "LiquidVisions.PanthaRhei.Tests" }
        };

        /// <summary>
        /// Executes the update core packages use case.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public Task<Response> Execute(string root)
        {
            Response response = new();

            try
            {
                IEnumerable<string> files = directory.GetFiles(root, "*.csproj", SearchOption.AllDirectories)
                    .Where(x => !x.Contains(".Template", StringComparison.InvariantCultureIgnoreCase));

                UpdatePackagesInAllProjectFiles(files);

                BuildAllSolutions(root);
            }
            catch (InvalidOperationException ex)
            {
                response.AddError(FaultCodes.InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }

            return Task.FromResult(response);

        }

        private void BuildAllSolutions(string root)
        {
            string[] solutionFiles = directory.GetFiles(root, "*.sln", SearchOption.AllDirectories);
            foreach (string solutionFile in solutionFiles)
            {
                string folder = file.GetDirectory(solutionFile);

                logger.Info($"Building solution file {solutionFile} to apply latest package update.");

                commandLine.Start("dotnet build", folder);
            }
        }

        private void UpdatePackagesInAllProjectFiles(IEnumerable<string> projectFiles)
        {
            foreach (string projectFile in projectFiles)
            {
                string projectFileDirectory = file.GetDirectory(projectFile);

                string package = projectFile
                    .EndsWith("tests.csproj", StringComparison.OrdinalIgnoreCase) ? packages["Tests"] : packages["Core"];

                string command = $"dotnet add package {package}";

                logger.Info($"Updating package {package} to latest version on {file.GetFileNameWithoutExtension(projectFile)}.csproj");

                commandLine.Start(command, projectFileDirectory, true);
            }
        }
    }
}
