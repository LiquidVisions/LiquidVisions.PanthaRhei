using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class VersionCommand : CommandLineApplication
    {
        public VersionCommand()
        {
            Name = "version";

            this.OnExecute(() =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fileVersionInfo.FileVersion;

                Console.WriteLine($"PanthaRhei flux CLI {version}");
            });
        }
    }
}
