// See https://aka.ms/new-console-template for more information
using System;
using LiquidVisions.PanthaRhei.Generator.Application;
using LiquidVisions.PanthaRhei.Generator.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Infrastructure;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework;
using LiquidVisions.PanthaRhei.Generator.Presentation.Cli;
using McMaster.Extensions.CommandLineUtils;
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

var runModeOption = cmd.Option<GenerationModes>(
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
    var provider = new ServiceCollection()
        .AddConsole()
        .AddDomainLayer()
        .AddApplicationLayer()
        .AddEntityFrameworkLayer()
        .AddInfrastructureLayer()
        .BuildServiceProvider();

    ExpandRequestModel expandRequestModel = provider.GetService<ExpandRequestModel>();
    expandRequestModel.AppId = Guid.Parse(appOption.Value());
    expandRequestModel.ConnectionString = dbOption.Value();
    expandRequestModel.ReSeed = reseed.HasValue();
    expandRequestModel.Root = rootOption.Value();
    expandRequestModel.Clean = cleanModeOption.HasValue();
    expandRequestModel.GenerationMode = runModeOption.ParsedValue == GenerationModes.None
        ? GenerationModes.Default
        : runModeOption.ParsedValue;

    provider.GetService<IExpandBoundary>()
        .Execute();
});

return cmd.Execute(args);
