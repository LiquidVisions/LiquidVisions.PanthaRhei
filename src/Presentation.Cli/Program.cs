// See https://aka.ms/new-console-template for more information
using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Presentation.Cli;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using (var cmd = new CommandLineApplication())
{
    cmd.HelpOption();

    CommandOption rootOption = cmd.Option(
        "--root",
        "Full path to the project root.",
        CommandOptionType.SingleValue)
        .IsRequired();

    CommandOption dbOption = cmd.Option(
        "--db",
        "The connectionstring that will be used.",
        CommandOptionType.SingleValue);

    CommandOption appOption = cmd.Option(
        "--app",
        "The id of the app.",
        CommandOptionType.SingleValue);

    CommandOption runModeOption = cmd.Option(
        "--mode",
        "The run mode determines the expander and handers that will be executed.",
        CommandOptionType.SingleValue);

    CommandOption<bool> cleanModeOption = cmd.Option<bool>(
        "--clean",
        "Deletes and discards all previous runs",
        CommandOptionType.SingleOrNoValue);

    CommandOption<bool> seed = cmd.Option<bool>(
        "--seed",
        "Sets a value indicating whether the database should be seeded with the data of the meta model",
        CommandOptionType.SingleOrNoValue);

    CommandOption<bool> migrate = cmd.Option<bool>(
        "--migrate",
        "Sets a value indicating whether the database schema should be attempted to update.",
        CommandOptionType.SingleOrNoValue);


    cmd.OnExecute(() =>
    {
        ExpandOptionsRequestModel requestModel = new()
        {
            AppId = Guid.Parse(appOption.Value()),
            Migrate = migrate.HasValue(),
            Seed = seed.HasValue(),
            Root = rootOption.Value(),
            Clean = cleanModeOption.HasValue(),
            GenerationMode = runModeOption.Value(),
            ConnectionString = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build()
                .GetConnectionString(dbOption.Value()),
        };

        ServiceProvider provider = new ServiceCollection()
            .AddPresentationLayer(requestModel)
            .BuildServiceProvider();

        provider.GetService<IExpandBoundary>()
            .Execute();
    });

    cmd.OnValidationError(x =>
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(x);
        Console.ResetColor();

        cmd.ShowHelp();
    });

    return await cmd.ExecuteAsync(args)
        .ConfigureAwait(true);
}
