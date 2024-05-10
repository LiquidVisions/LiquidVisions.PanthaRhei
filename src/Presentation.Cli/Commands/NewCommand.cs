using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class NewCommand : CommandLineApplication
    {
        public NewCommand()
        {
            base.Name = "new";

            using var newExpanderCommand = new NewExpanderCommand();
            AddSubcommand(newExpanderCommand);
        }
    }

    internal class NewExpanderCommand : CommandLineApplication
    {
        public NewExpanderCommand()
        {
            base.Name = "expander";

            CommandOption nameOption = Option(
                "--shortName",
                "Name of the new expander class.",
                CommandOptionType.SingleValue)
                .IsRequired();

            CommandOption typeOption = Option(
                "--fullName",
                "Type of the new expander solution.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            CommandOption pathOption = Option(
                "--path",
                "Path where the expander will be created.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            CommandOption buildPathOption = Option(
                "--buildPath",
                "Path where the expander will outputs' its builds.",
                CommandOptionType.SingleValue)
                .IsRequired(true);

            CommandOption<bool> buildOption = this.Option<bool>(
                "--build",
                "Builds the newly created expander",
                CommandOptionType.SingleOrNoValue);

            this.OnExecute(() =>
            {
                NewExpanderRequestModel model = new()
                {
                    ShortName = nameOption.Value(),
                    fullName = typeOption.Value(),
                    Path = pathOption.Value(),
                    BuildPath = buildPathOption.Value(),
                    Build = buildOption.HasValue()
                };

                ServiceProvider provider = new ServiceCollection()
                    .AddPresentationLayer()
                    .BuildServiceProvider();

                provider.GetService<IBoundary>()
                    .CreateNewExpander(model);
            });
        }
    }
}
