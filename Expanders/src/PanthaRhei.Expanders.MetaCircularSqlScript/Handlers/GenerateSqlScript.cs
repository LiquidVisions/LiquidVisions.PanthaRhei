﻿using System;
using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using LiquidVisions.PanthaRhei.Generator.Domain.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.MetaCircularSqlScript.Handlers
{
    public class GenerateSqlScript : AbstractScaffoldDotNetTemplateHandler<MetaCircularSqlScriptExpander>
    {
        private readonly ITemplateService templateService;
        private readonly IFileService fileService;
        private readonly IGenericRepository<DataType> dataTypeRepository;

        public GenerateSqlScript(MetaCircularSqlScriptExpander expander, IDependencyResolver dependencyResolver)
            : base(expander, dependencyResolver)
        {
            templateService = dependencyResolver.Get<ITemplateService>();
            fileService = dependencyResolver.Get<IFileService>();
            dataTypeRepository = dependencyResolver.Get<IGenericRepository<DataType>>();
        }

        public override void Execute()
        {
            Type[] entityTypes = new[]
            {
                typeof(App),
            };

            string fullPathToTemplateFile = Path.Combine(
                Parameters.ExpandersFolder,
                Expander.Model.Name,
                ".Templates",
                "MetaCircularSqlScriptTemplate.template");

            string sqlScript = templateService.Render(
                fullPathToTemplateFile,
                new
                {
                    entityTypes,
                    App,
                    DataTypes = dataTypeRepository.GetAll(),
                });

            fileService.WriteAllText(Path.Combine(Parameters.OutputFolder, "seed.sql"), sqlScript);
        }
    }
}
