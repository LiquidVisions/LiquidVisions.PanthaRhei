using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New
{
    internal class NewCommand : PanthaRheiCommandLineApplication
    {
        public NewCommand()
        {
            Name = "new";
            HelpOption("-?", true);

            IDependencyFactory factory = new ServiceCollection()
            .AddPresentationLayer()
            .BuildServiceProvider()
                .GetService<IDependencyFactory>();


            using var newExpanderCommand = new NewExpanderCommand(factory);
            AddSubcommand(newExpanderCommand);
        }
    }
}
