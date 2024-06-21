﻿using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New
{
    internal class NewCommand : PanthaRheiCommandLineApplication
    {
        public NewCommand()
        {
            Name = "new";
            HelpOption("-?", true);

            using var newExpanderCommand = new NewExpanderCommand();
            AddSubcommand(newExpanderCommand);
        }
    }
}
