using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update
{
    internal class UpdateCommand : CommandLineApplication
    {
        public UpdateCommand()
        {
            Name = "update";
            HelpOption("-?", true);

            using var updatePackagesCommand = new UpdatePackages();
            using var updateCoreCommand = new UpdateCoreCommand();

            AddSubcommand(updatePackagesCommand);
            AddSubcommand(updateCoreCommand);
        }
    }
}
