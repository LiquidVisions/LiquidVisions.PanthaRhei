using System;
using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal abstract class CommandLineApplicationBase : CommandLineApplication
    {
        public CommandLineApplicationBase()
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
