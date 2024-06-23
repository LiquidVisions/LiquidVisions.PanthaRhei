using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class HarvestCommand : CommandLineApplication
    {
        public HarvestCommand()
        {
            base.Name = "harvest";
            HelpOption("-?", true);
        }
    }
}
