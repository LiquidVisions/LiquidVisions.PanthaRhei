using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Presentation.Cli.UseCases;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.Set
{
    internal class SetConnectionStringCommand : CommandLineApplication
    {
        public SetConnectionStringCommand()
        {
            Name = "database";

            CommandOption name = Option(
                "-n|--name",
                "The name of the database",
                CommandOptionType.SingleValue)
                .IsRequired();

            CommandOption connection = Option(
                "-c|--connection",
                "The ConnectionString of the database",
                CommandOptionType.SingleValue);

            this.OnExecute(() =>
            {

                SetDatabaseCommandModel requestModel = new()
                {
                    Name = name.Value(),
                    ConnectionString = connection.Value()
                };

                ServiceProvider provider = new ServiceCollection()
                    .AddPresentationLayer()
                    .BuildServiceProvider();

                provider.GetService<ICommand<SetDatabaseCommandModel>>()
                    .Execute(requestModel);

            });
        }
    }
}
