// See https://aka.ms/new-console-template for more information
using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Presentation.Cli;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var cmd = new CommandLineApplication();

cmd.HelpOption();

var rootOption = cmd.Option(
    "--root",
    "Full path to the project root.",
    CommandOptionType.SingleValue)
    .IsRequired();

var dbOption = cmd.Option(
    "--db",
    "The connectionstring that will be used.",
    CommandOptionType.SingleValue);

var appOption = cmd.Option(
    "--app",
    "The id of the app.",
    CommandOptionType.SingleValue);

var runModeOption = cmd.Option(
    "--mode",
    "The run mode determines the expander and handers that will be executed.",
    CommandOptionType.SingleValue);

var cleanModeOption = cmd.Option<bool>(
    "--clean",
    "Deletes and discards all previous runs",
    CommandOptionType.SingleOrNoValue);

var seed = cmd.Option<bool>(
    "--seed",
    "Sets a value indicating whether the database should be seeded with the data of the meta model",
    CommandOptionType.SingleOrNoValue);

var migrate = cmd.Option<bool>(
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

    var provider = new ServiceCollection()
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

return await cmd.ExecuteAsync(args);
