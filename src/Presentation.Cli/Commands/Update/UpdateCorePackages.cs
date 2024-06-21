using LiquidVisions.PanthaRhei.Application.Boundaries;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update
{
    internal class UpdateCorePackages : PanthaRheiCommandLineApplication
    {
        private readonly CommandOption rootOption;

        public UpdateCorePackages()
        {
            Name = "core";
            HelpOption("-?", true);

            rootOption = Option(
                "--root",
                "Full path to the project root.",
                CommandOptionType.SingleValue)
                .IsRequired();
        }

        public override void OnExecute() => new ServiceCollection()
            .AddPresentationLayer()
            .BuildServiceProvider()
            .GetService<IBoundary>()
            .UpdateCorePackages(rootOption.Value());
    }
}
