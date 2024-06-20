using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetAppCommand : PanthaRheiCommandLineApplication
    {
        private readonly IRunSettings settings;
        private readonly CommandOption app;

        public SetAppCommand(IRunSettings settings)
        {
            this.settings = settings;
            Name = "app";

            app = Option(
                "-a|--app",
                "The app identifier",
                CommandOptionType.SingleValue)
                .IsRequired();
        }

        public override void OnExecute()
            => settings.Set("RunSettings", "App", app.Value());
    }
}
