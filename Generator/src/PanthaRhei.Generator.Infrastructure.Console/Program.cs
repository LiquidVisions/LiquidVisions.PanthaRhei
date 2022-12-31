// See https://aka.ms/new-console-template for more information
using LiquidVisions.PanthaRhei.Generator.Application;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Infrastructure;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.Console;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

var cmd = new CommandLineApplication();

cmd.OnExecute(() =>
{
    var provider = new ServiceCollection()
        .AddConsole()
        .AddDomainLayer()
        .AddApplicationLayer()
        .AddInfrastructureLayer()
        .BuildServiceProvider();

    //RunParameters parameters = provider.GetService<RunParameters>();
    //parameters.Root = rootOption.Value();
    //parameters.ModelName = modelOption.Value();
    //parameters.Clean = cleanModeOption.HasValue();
    //parameters.RunMode = runModeOption.ParsedValue == Mode.None
    //    ? Mode.Default
    //    : runModeOption.ParsedValue;

    //provider.GetService<IApplicationService>().Handle();
});

return cmd.Execute(args);
