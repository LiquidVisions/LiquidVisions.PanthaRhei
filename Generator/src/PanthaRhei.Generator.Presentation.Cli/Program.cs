// See https://aka.ms/new-console-template for more information
using System;
using LiquidVisions.PanthaRhei.Generator.Application;
using LiquidVisions.PanthaRhei.Generator.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generator.Application.RequestModels;
using LiquidVisions.PanthaRhei.Generator.Presentation.Cli;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var cmd = new CommandLineApplication();

cmd.HelpOption();

var rootOption = cmd.Option(
    "--root",
    "Full path to the project root.",
    CommandOptionType.SingleValue);

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

var reseed = cmd.Option<bool>(
    "--reseed",
    "Reinitialze current model based",
    CommandOptionType.SingleOrNoValue);

cmd.OnExecute(() =>
{
    ExpandOptionsRequestModel expandRequestModel = new()
    {
        AppId = Guid.Parse(appOption.Value()),
        ReSeed = reseed.HasValue(),
        Root = rootOption.Value(),
        Clean = cleanModeOption.HasValue(),
        GenerationMode = runModeOption.Value(),
        ConnectionString = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build()
            .GetConnectionString(dbOption.Value()),
    };

    var provider = new ServiceCollection()
        .AddApplicationLayer(expandRequestModel)
        .BuildServiceProvider();

    provider.GetService<IExpandBoundary>()
        .Execute();
});

return cmd.Execute(args);
