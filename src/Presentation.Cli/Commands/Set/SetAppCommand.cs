﻿using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetAppCommand : CommandLineApplicationBase
    {
        private readonly IRunSettings settings;
        private readonly CommandOption app;

        public SetAppCommand(IRunSettings settings)
        {
            this.settings = settings;
            Name = "app";
            HelpOption("-?", true);

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
