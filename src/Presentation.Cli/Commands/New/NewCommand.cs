using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands.New
{
    internal class NewCommand : CommandLineApplicationBase
    {
        public NewCommand(IDependencyFactory dependencyFactory)
        {
            Name = "new";
            HelpOption("-?", true);

            using var newExpanderCommand = new NewExpanderCommand(dependencyFactory);
            AddSubcommand(newExpanderCommand);
        }
    }
}
