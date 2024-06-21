using LiquidVisions.PanthaRhei.Application.Boundaries;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update
{
    internal class UpdateCoreCommand : PanthaRheiCommandLineApplication
    {
        public UpdateCoreCommand()
        {
            Name = "core";
            HelpOption("-?", true);
        }

        public override void OnExecute() => new ServiceCollection()
            .AddPresentationLayer()
            .BuildServiceProvider()
            .GetService<IBoundary>()
                .UpdateCore();
    }
}
