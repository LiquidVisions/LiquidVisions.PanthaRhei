﻿using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetRootSettingsCommand : CommandLineApplicationBase
    {
        private readonly IRunSettings settings;
        private readonly CommandOption root;

        public SetRootSettingsCommand(IRunSettings settings)
        {
            this.settings = settings;
            Name = "root";
            HelpOption("-?", true);

            root = Option(
                "-r|--root",
                "The name of the database",
                CommandOptionType.SingleValue)
                .IsRequired();
        }

        public override void OnExecute()
            => settings.Set("RunSettings", "Root", root.Value());
    }
}
