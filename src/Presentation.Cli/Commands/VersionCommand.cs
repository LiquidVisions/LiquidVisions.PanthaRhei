using System;
using System.Diagnostics;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class VersionCommand : PanthaRheiCommandLineApplication
    {
        public VersionCommand()
        {
            Name = "version";
            HelpOption("-?", true);
        }

        public override void OnExecute()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.FileVersion;

            Console.WriteLine($"Version: {version}");
        }
    }
}
