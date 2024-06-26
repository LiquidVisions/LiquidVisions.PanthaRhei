﻿using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli.Commands
{
    internal class ExpandSubCommand : CommandLineApplicationBase
    {
        private readonly CommandOption rootOption;
        private readonly CommandOption appOption;
        private readonly CommandOption runModeOption;
        private readonly CommandOption<bool> cleanModeOption;
        private readonly CommandOption<bool> seed;
        private readonly CommandOption<bool> migrate;
        private readonly IDependencyFactory dependencyFactory;

        public ExpandSubCommand(IDependencyFactory dependencyFactory)
        {
            base.Name = "expand";
            base.HelpOption("-?");
            rootOption = Option(
                    "--root",
                    "Full path to the project root.This option is optional. Use the command flux set -r <PATH> to set the default output folder in the run settings.",
                    CommandOptionType.SingleValue);

            appOption = Option(
                "--app",
                "The identifier of the App. This option is optional. Use the command flux set app -a <APPID> to set the default in the run settings.",
                CommandOptionType.SingleValue);

            runModeOption = Option(
                "--mode",
                "The run mode determines the expander and handers that will be executed.",
                CommandOptionType.SingleValue);

            cleanModeOption = this.Option<bool>(
                "--clean",
                "Deletes and discards all previous runs",
                CommandOptionType.SingleOrNoValue);

            seed = this.Option<bool>(
                "--seed",
                "Sets a value indicating whether the database should be seeded with the data of the meta model",
                CommandOptionType.SingleOrNoValue);

            migrate = this.Option<bool>(
                "--migrate",
                "Sets a value indicating whether the database schema should be attempted to update.",
                CommandOptionType.SingleOrNoValue);
            this.dependencyFactory = dependencyFactory;
        }

        public override void OnExecute()
        {
            GenerationOptions model = dependencyFactory
                .Resolve<GenerationOptions>();

            model.AppId = string.IsNullOrEmpty(appOption.Value()) ? model.AppId : Guid.Parse(appOption.Value());
            model.Migrate = migrate.HasValue();
            model.Seed = seed.HasValue();
            model.Root = rootOption.Value() != null ? rootOption.Value() : model.Root;
            model.Clean = cleanModeOption.HasValue();
            model.Modes = runModeOption.Value() != null ? Enum.Parse<GenerationModes>(runModeOption.Value()) : GenerationModes.None;

            dependencyFactory.Resolve<IExpandBoundary>()
                .Execute();
        }
    }
}
