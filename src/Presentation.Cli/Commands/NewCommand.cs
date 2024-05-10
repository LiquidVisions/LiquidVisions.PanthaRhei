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
                "--name",
                "Name of the new expander.",
                CommandOptionType.SingleValue)
                .IsRequired();

            CommandOption typeOption = Option(
                "--type",
                "Type of the new expander.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            CommandOption pathOption = Option(
                "--path",
                "Path where the expander will be created.",
                CommandOptionType.SingleValue)
                .IsRequired(false);

            this.OnExecute(() =>
            {
                NewExpanderRequestModel model = new()
                {
                    Name = nameOption.Value(),
                    Type = typeOption.Value(),
                    Path = pathOption.Value()
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
