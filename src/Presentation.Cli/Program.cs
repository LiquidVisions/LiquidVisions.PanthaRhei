// See https://aka.ms/new-console-template for more information
using System;
using LiquidVisions.PanthaRhei.Presentation.Cli.Commands;
using McMaster.Extensions.CommandLineUtils;

using PanthaRheiApp app = new();

app.OnValidationError(x =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(x);
    Console.ResetColor();

    app.ShowHelp();
});

return app.Execute(args);
