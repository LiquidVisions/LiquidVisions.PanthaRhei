using LiquidVisions.PanthaRhei.Application.Boundaries;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update
{
    internal class UpdatePackages : PanthaRheiCommandLineApplication
    {
        private readonly CommandOption rootOption;

        public UpdatePackages()
        {
            Name = "packages";
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
                .UpdatePackages(rootOption.Value());
    }
}
