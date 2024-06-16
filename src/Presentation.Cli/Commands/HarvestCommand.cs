using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class HarvestCommand : CommandLineApplication
    {
        public HarvestCommand()
        {
            base.Name = "harvest";

        }
    }
}
