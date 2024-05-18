using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class NewCommand : CommandLineApplication
    {
        public NewCommand()
        {
            base.Name = "new";

            using var newExpanderCommand = new NewExpanderCommand();
            AddSubcommand(newExpanderCommand);
        }
    }
}
