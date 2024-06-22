using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Repositories;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander
{
    internal class CreateNewExpander(
        ICommandLine commandLine,
        IDirectory directory,
        IFile file,
        ILogger logger,
        ICreateRepository<Expander> createRepository,
        IGetRepository<App> getAppRepository,
        IUpdateRepository<App> updateAppRepository) : ICreateNewExpander
    {

        public Task<Response> Execute(CreateNewExpanderRequestModel model)
        {
            Response response = new();

            App app = getAppRepository.GetById(model.AppId);
            if(app.Expanders.Any(x => x.Name == model.FullName))
            {
                response.AddError(FaultCodes.BadRequest, $"Expander with name {model.FullName} already exists.");
                return Task.FromResult(response);
            }

            InstallCommand();
            TemplateCommand(model);
            RenameTemplateDirectory(model);
            RenameTemplateFile(model);

            if (model.Build)
            {
                Build(model);
            }

            UnInstallCommand();

            Expander expander = new()
            {
                Id = Guid.NewGuid(),
                Name = model.FullName,
                Enabled = true,
                Order = 0,
            };

            createRepository.Create(expander);
            
            app.Expanders.Add(expander);

            updateAppRepository.Update(app);

            return Task.FromResult(response);

        }

        private void Build(CreateNewExpanderRequestModel model) => commandLine.Start("dotnet build", Path.Combine(model.Path, model.FullName));

        private void RenameTemplateFile(CreateNewExpanderRequestModel model)
        {
            string[] searchResult = directory.GetFiles(model.Path, ".template.json", SearchOption.AllDirectories);

            if (!AssertAndGetFullPath(".template.json", "template.json", searchResult, out string source, out string target))
            {
                string errorMessages = $"Expected to find one file named .template.json but found {searchResult.Length}.";
                logger.Fatal(errorMessages);

                throw new InvalidOperationException(errorMessages);
            }

            logger.Trace($"Renaming template directory {source} to {target}");

            file.Rename(source, target);
        }

        private void RenameTemplateDirectory(CreateNewExpanderRequestModel model)
        {
            string[] searchResult = directory.GetDirectories(model.Path, "_template.config", SearchOption.AllDirectories);

            if (!AssertAndGetFullPath("_template.config", ".template.config", searchResult, out string source, out string target))
            {
                string errorMessages = $"Expected to find one directory named _template.config but found {searchResult.Length}.";
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

        private void TemplateCommand(CreateNewExpanderRequestModel model)
        {
            string command = $"dotnet new expander -n {model.FullName} --buildPath {model.BuildPath} --shortName {model.ShortName} -d";

            commandLine.Start(command, model.Path ?? null, true);
        }

        private void InstallCommand() => commandLine.Start($"dotnet new install {Resources.TemplatePackageName} --force", true);
        private void UnInstallCommand() => commandLine.Start($"dotnet new uninstall {Resources.TemplatePackageName}", true);
    }
}
