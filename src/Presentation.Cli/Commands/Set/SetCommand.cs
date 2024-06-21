using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetCommand : CommandLineApplication
    {
        private static readonly string path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "appsettings.json");

        public SetCommand()
        {
            Name = "set";
            HelpOption("-?", true);

            IRunSettings runSettings = new ServiceCollection()
                .AddPresentationLayer()
                .BuildServiceProvider()
                .GetService<IRunSettings>();

            runSettings.Path = path;

            using var setConnectionStringCommand = new SetConnectionStringCommand(runSettings);
            using var setRootCommand = new SetRootSettingsCommand(runSettings);
            using var setAppCommand = new SetAppCommand(runSettings);

            AddSubcommand(setConnectionStringCommand);
            AddSubcommand(setRootCommand);
            AddSubcommand(setAppCommand);
        }
    }
}
