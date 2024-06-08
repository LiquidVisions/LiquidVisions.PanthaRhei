// See https://aka.ms/new-console-template for more information
using System;
using LiquidVisions.PanthaRhei.Presentation.Cli.Commands;
using McMaster.Extensions.CommandLineUtils;

using var app = new CommandLineApplication();
using var buildCommand = new ExpandSubCommand();
using var newCommand = new NewCommand();
using var versionCommand = new VersionCommand();
using var updateCommand = new UpdateCommand();

app.HelpOption();
app.AddSubcommand(buildCommand);
app.AddSubcommand(newCommand);
app.AddSubcommand(updateCommand);
app.AddSubcommand(versionCommand);

app.OnExecute(() =>
{
    app.ValidationErrorHandler = (result) =>
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(result.ErrorMessage);
        Console.ResetColor();

        app.ShowHelp();

        return 1;
    };

    Console.WriteLine("Specify a subcommand");
    app.ShowHelp();
    return 1;
});

app.OnValidationError(x =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(x);
    Console.ResetColor();

    app.ShowHelp();
});

return app.Execute(args);
