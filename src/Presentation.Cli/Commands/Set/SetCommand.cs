using System.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetCommand : CommandLineApplication
    {
        private static readonly string path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "appsettings.json");

        public SetCommand(IDependencyFactory dependencyFactory)
        {
            Name = "set";
            HelpOption("-?", true);

            IRunSettings runSettings = dependencyFactory
                .Resolve<IRunSettings>();

            runSettings.Path = path;

            using var setConnectionStringCommand = new SetDatabaseCommand(runSettings);
            using var setRootCommand = new SetRootSettingsCommand(runSettings);
            using var setAppCommand = new SetAppCommand(runSettings);

            AddSubcommand(setConnectionStringCommand);
            AddSubcommand(setRootCommand);
            AddSubcommand(setAppCommand);
        }
    }
}
