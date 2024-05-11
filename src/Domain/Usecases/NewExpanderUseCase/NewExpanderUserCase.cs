using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.NewExpanderUseCase
{
    internal class NewExpanderUserCase(ICommandLine commandLine, IDirectory directory, IFile file, ILogger logger) : INewExpanderUseCase
    {
        private readonly ICommandLine commandLine = commandLine;
        private readonly IDirectory directory = directory;
        private readonly ILogger logger = logger;
        private readonly string templatePackage = "LiquidVisions.PanthaRhei.Templates.Expander";

        public Task<Response> Execute(NewExpander model)
        {
            Response response = new();

            InstallCommand();
            TemplateCommand(model);
            RenameTemplateDirectory(model);
            RenameTemplateFile(model);
            Build(model);
            UnInstallCommand();

            return Task.FromResult(response);

        }

        private void Build(NewExpander model) => commandLine.Start("dotnet build", Path.Combine(model.Path, model.FullName));

        private void RenameTemplateFile(NewExpander model)
        {
            string[] searchResult = directory.GetFiles(model.Path, ".template.json", SearchOption.AllDirectories);

            if (!AssertAndGetFullPath(".template.json", "template.json", searchResult, out string source, out string target))
            {
                string errorMessages = $"Expected to find one file named {source} but found {searchResult.Length}.";
                logger.Fatal(errorMessages);

                throw new InvalidOperationException(errorMessages);
            }

            logger.Trace($"Renaming template directory {source} to {target}");

            file.Rename(source, target);
        }

        private void RenameTemplateDirectory(NewExpander model)
        {
            string[] searchResult = directory.GetDirectories(model.Path, "_template.config", SearchOption.AllDirectories);

            if(!AssertAndGetFullPath("_template.config", ".template.config", searchResult, out string source, out string target))
            {
                string errorMessages = $"Expected to find one directory named {source} but found {searchResult.Length}.";
                logger.Fatal(errorMessages);

                throw new InvalidOperationException(errorMessages);
            }

            logger.Trace($"Renaming template directory {source} to {target}");

            directory.Rename(source, target);
        }

        private static bool AssertAndGetFullPath(string source, string target, string[] searchResult, out string sourceOut, out string targetOut)
        {
            sourceOut = string.Empty;
            targetOut = string.Empty;

            if (searchResult.Length != 1)
            {
                return false;
            }

            sourceOut = searchResult.Single();
            targetOut = searchResult.Single().Replace(source, target, StringComparison.OrdinalIgnoreCase);

            return true;
        }

        private void TemplateCommand(NewExpander model)
        {
            string command = $"dotnet new expander -n {model.FullName} --buildPath {model.BuildPath} --shortName {model.ShortName} -d";

            commandLine.Start(command, model.Path ?? null, true);
        }

        private void InstallCommand() => commandLine.Start($"dotnet new install {templatePackage} --force", true);
        private void UnInstallCommand() => commandLine.Start($"dotnet new uninstall {templatePackage}", true);
    }
}
