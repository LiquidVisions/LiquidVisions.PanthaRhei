using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.NewExpanderUseCase
{
    internal class NewExpanderUserCase(ICommandLine commandLine, IDirectory directory, ILogger logger) : INewExpanderUseCase
    {
        private readonly ICommandLine commandLine = commandLine;
        private readonly IDirectory directory = directory;
        private readonly ILogger logger = logger;

        public Task<Response> Execute(NewExpander model)
        {
            Response response = new();

            string sourceTemplateDirectoryName = "_template.config";
            string targetTemplateDirectoryName = ".template.config";

            string command = $"dotnet new expander -n {model.FullName} --buildPath {model.BuildPath} --shortName {model.ShortName} -d";

            commandLine.Start(command, model.Path ?? null);


            string[] searchResult = directory.GetDirectories(model.Path, sourceTemplateDirectoryName, SearchOption.AllDirectories);
            if(searchResult.Length != 1)
            {
                string errorMessages = $"Expected to find one directory named {sourceTemplateDirectoryName} but found {searchResult.Length}.";
                logger.Fatal(errorMessages);
                response.AddError(ErrorCodes.InternalServerError, errorMessages);

                return Task.FromResult(response);
            }

            string source = searchResult.Single();
            string target = searchResult.Single().Replace(sourceTemplateDirectoryName, targetTemplateDirectoryName, StringComparison.OrdinalIgnoreCase);

            logger.Trace($"Renaming template directory {source} to {target}");

            directory.Rename(source, target);

            commandLine.Start("dotnet build", Path.Combine(model.Path, model.FullName));

            return Task.FromResult(response);

        }
    }
}
