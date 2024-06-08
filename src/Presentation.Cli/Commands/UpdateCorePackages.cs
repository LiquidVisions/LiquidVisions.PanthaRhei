﻿using LiquidVisions.PanthaRhei.Application.Boundaries;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class UpdateCorePackages : CommandLineApplication
    {
        public UpdateCorePackages()
        {
            base.Name = "core";

            CommandOption rootOption = Option(
                "--root",
                "Full path to the project root.",
                CommandOptionType.SingleValue)
                .IsRequired();

            this.OnExecute(() =>
            {
                ServiceProvider provider = new ServiceCollection()
                    .AddPresentationLayer()
                    .BuildServiceProvider();

                provider.GetService<IBoundary>()
                    .UpdateCorePackages(rootOption.Value());
            });
        }
    }
}
