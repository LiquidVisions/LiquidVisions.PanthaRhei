using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New
{
    internal class NewCommand : CommandLineApplication
    {
        public NewCommand()
        {
            Name = "new";

            using var newExpanderCommand = new NewExpanderCommand();
            AddSubcommand(newExpanderCommand);
        }
    }
}
