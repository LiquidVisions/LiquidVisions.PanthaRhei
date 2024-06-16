using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetCommand : CommandLineApplication
    {
        public SetCommand()
        {
            Name = "set";
            using var connectionStringCommand = new SetConnectionStringCommand();
            AddSubcommand(connectionStringCommand);
        }
    }
}
