using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update
{
    internal class UpdateCommand : CommandLineApplication
    {
        public UpdateCommand(IDependencyFactory dependencyFactory)
        {
            Name = "update";
            HelpOption("-?", true);

            using var updatePackagesCommand = new UpdatePackages(dependencyFactory);

            AddSubcommand(updatePackagesCommand);
        }
    }
}
