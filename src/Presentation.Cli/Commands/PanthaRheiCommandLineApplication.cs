using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Identity.Client;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal abstract class PanthaRheiCommandLineApplication : CommandLineApplication
    {
        public PanthaRheiCommandLineApplication()
        {
            this.OnExecute(() => OnExecute());

            this.OnValidationError(x =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(x);
                Console.ResetColor();

                ShowHelp();
            });
        }

        public virtual void OnExecute() { } 
    }
}
