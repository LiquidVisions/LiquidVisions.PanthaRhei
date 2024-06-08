using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class UpdateCommand : CommandLineApplication
    {
        public UpdateCommand()
        {
            base.Name = "update";

            using var updatePackagesCommand = new UpdateCorePackages();
            AddSubcommand(updatePackagesCommand);
        }
    }
}
