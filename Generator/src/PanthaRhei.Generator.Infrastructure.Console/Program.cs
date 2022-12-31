// See https://aka.ms/new-console-template for more information
using LiquidVisions.PanthaRhei.Generator.Application;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using LiquidVisions.PanthaRhei.Generator.Infrastructure;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.Console;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

var cmd = new CommandLineApplication();

var runModeOption = cmd.Option<GenerationModes>(
    "--mode",
    "The run mode determines the expander and handers that will be executed.",
    CommandOptionType.SingleValue);

var cleanModeOption = cmd.Option<bool>(
    "--clean",
    "Deletes and discards all previous runs",
    CommandOptionType.SingleOrNoValue);

cmd.OnExecute(() =>
{
    var provider = new ServiceCollection()
        .AddConsole()
        .AddDomainLayer()
        .AddApplicationLayer()
        .AddInfrastructureLayer()
        .BuildServiceProvider();

    GeneratorParameters parameters = provider.GetService<GeneratorParameters>();
    parameters.Clean = cleanModeOption.HasValue();
    parameters.GenerationMode = runModeOption.ParsedValue == GenerationModes.None
        ? GenerationModes.Default
        : runModeOption.ParsedValue;

    provider.GetService<IGeneratorService>().Handle();
});

return cmd.Execute(args);
