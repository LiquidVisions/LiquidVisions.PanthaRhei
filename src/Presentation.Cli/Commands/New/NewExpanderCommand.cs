using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New
{
    internal class NewExpanderCommand : PanthaRheiCommandLineApplication
    {
        private readonly CommandOption nameOption;
        private readonly CommandOption typeOption;
        private readonly CommandOption pathOption;
        private readonly CommandOption buildPathOption;
        private readonly CommandOption<bool> buildOption;


        public NewExpanderCommand()
        {
            Name = "expander";

            nameOption = Option(
                "--shortName",
                "Name of the new expander class.",
                CommandOptionType.SingleValue)
                .IsRequired();

            typeOption = Option(
                "--fullName",
                "Type of the new expander solution.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            pathOption = Option(
                "--path",
                "Path where the expander will be created.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            buildPathOption = Option(
                "--buildPath",
                "Path where the expander will outputs' its builds.",
                CommandOptionType.SingleValue)
                .IsRequired(true);

            buildOption = this.Option<bool>(
                "--build",
                "Builds the newly created expander",
                CommandOptionType.SingleOrNoValue);
        }

        public override void OnExecute()
        {
            new ServiceCollection()
                .AddPresentationLayer()
                .BuildServiceProvider()
                .GetService<IBoundary>()
                .CreateNewExpander(new NewExpanderRequestModel {
                    ShortName = nameOption.Value(),
                    FullName = typeOption.Value(),
                    Path = pathOption.Value(),
                    BuildPath = buildPathOption.Value(),
                    Build = buildOption.HasValue()
                });
        }
    }
}
