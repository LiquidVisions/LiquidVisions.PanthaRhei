using System;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New;
using LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set;
using LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class PanthaRheiApp : CommandLineApplication
    {
        private readonly IDependencyFactory dependencyFactory = new ServiceCollection()
            .AddPresentationLayer()
            .BuildServiceProvider()
            .GetService<IDependencyFactory>();

        public PanthaRheiApp()
        {
            using var buildCommand = new ExpandSubCommand(dependencyFactory);
            using var newCommand = new NewCommand(dependencyFactory);
            using var versionCommand = new VersionCommand();
            using var updateCommand = new UpdateCommand(dependencyFactory);
            using var setCommand = new SetCommand(dependencyFactory);

            HelpOption("-?");
            AddSubcommand(buildCommand);
            AddSubcommand(newCommand);
            AddSubcommand(updateCommand);
            AddSubcommand(versionCommand);
            AddSubcommand(setCommand);

            OnExecute(() =>
            {
                ValidationErrorHandler = (result) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.ErrorMessage);
                    Console.ResetColor();

                    ShowHelp();

                    return 1;
                };

                Console.WriteLine("Specify a subcommand");
                ShowHelp();
                return 1;
            });
        }
    }
}
