using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class ExpandSubCommand : CommandLineApplication
    {
        public ExpandSubCommand()
        {
            base.Name = "expand";

            CommandOption rootOption = Option(
                    "--root",
                    "Full path to the project root.",
                    CommandOptionType.SingleValue);

            CommandOption appOption = Option(
                "--app",
                "The id of the app.",
                CommandOptionType.SingleValue);

            CommandOption runModeOption = Option(
                "--mode",
                "The run mode determines the expander and handers that will be executed.",
                CommandOptionType.SingleValue);

            CommandOption<bool> cleanModeOption = this.Option<bool>(
                "--clean",
                "Deletes and discards all previous runs",
                CommandOptionType.SingleOrNoValue);

            CommandOption<bool> seed = this.Option<bool>(
                "--seed",
                "Sets a value indicating whether the database should be seeded with the data of the meta model",
                CommandOptionType.SingleOrNoValue);

            CommandOption<bool> migrate = this.Option<bool>(
                "--migrate",
                "Sets a value indicating whether the database schema should be attempted to update.",
                CommandOptionType.SingleOrNoValue);

            this.OnExecute(() =>
            {
                ExpandOptionsRequestModel requestModel = new()
                {
                    AppId = Guid.Parse(appOption.Value()),
                    Migrate = migrate.HasValue(),
                    Seed = seed.HasValue(),
                    Root = rootOption.Value(),
                    Clean = cleanModeOption.HasValue(),
                    GenerationMode = runModeOption.Value(),
                };

                ServiceProvider provider = new ServiceCollection()
                    .AddPresentationLayer(requestModel)
                    .BuildServiceProvider();

                provider.GetService<IExpandBoundary>()
                    .Execute();
            });

            this.OnValidationError(x =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(x);
                Console.ResetColor();

                ShowHelp();
            });
        }
    }
}
