using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetConnectionStringCommand : PanthaRheiCommandLineApplication
    {
        private readonly CommandOption name;
        private readonly CommandOption connection;
        private readonly IRunSettings runSettings;

        public SetConnectionStringCommand(IRunSettings runSettings)
        {
            this.runSettings = runSettings;
            Name = "database";
            HelpOption("-?", true);

            name = Option(
                "-n|--name",
                "The name of the database",
                CommandOptionType.SingleValue)
                .IsRequired();

            connection = Option(
                "-c|--connection",
                "The ConnectionString of the database",
                CommandOptionType.SingleValue);
        }

        public override void OnExecute()
        {
            runSettings.Set("ConnectionStrings", name.Value(), connection.Value());
        }
    }
}
