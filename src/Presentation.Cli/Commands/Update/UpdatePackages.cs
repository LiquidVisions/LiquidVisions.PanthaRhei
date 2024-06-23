using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Update
{
    internal class UpdatePackages : CommandLineApplicationBase
    {
        private readonly CommandOption rootOption;
        private readonly IDependencyFactory dependencyFactory;

        public UpdatePackages(IDependencyFactory dependencyFactory)
        {
            Name = "packages";
            HelpOption("-?", true);

            rootOption = Option(
                "--root",
                "Full path to the project root.",
                CommandOptionType.SingleValue)
                .IsRequired();
            this.dependencyFactory = dependencyFactory;
        }

        public override void OnExecute() => dependencyFactory
            .Resolve<IBoundary>()
            .UpdatePackages(rootOption.Value());
    }
}
