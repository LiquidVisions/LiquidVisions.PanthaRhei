using System;
using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    internal class InitializeComponentsUseCase : IInitializeComponentsUseCase
    {
        private readonly IGenericRepository<Component> repository;
        private readonly Parameters parameters;
        private readonly IDirectoryService directoryService;
        private readonly IFileService fileService;

        public InitializeComponentsUseCase(IDependencyResolver dependencyResolver)
        {
            this.repository = dependencyResolver.Get<IGenericRepository<Component>>();
            this.parameters = dependencyResolver.Get<Parameters>();
            this.directoryService = dependencyResolver.Get<IDirectoryService>();
            this.fileService = dependencyResolver.Get<IFileService>();
        }

        public void Initialize(IEnumerable<Expander> expanders)
        {
            foreach (Expander expander in expanders)
            {
                string templatePath = Path.Combine(parameters.ExpandersFolder, expander.Name, expander.TemplateFolder);
                if (directoryService.Exists(templatePath))
                {
                    string[] files = directoryService.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories);
                    if (files != null && files.Length > 0)
                    {
                        foreach (string file in files)
                        {
                            string fileName = fileService.GetFileNameWithoutExtension(file);
                            string componentName = fileName.Split('.')[1];

                            Component component = new()
                            {
                                Id = Guid.NewGuid(),
                                Name = componentName,
                                Ent
                            };

                            repository.Create(component);
                        }
                    }
                }
            }
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidOperationException($"Failed to delete all the {nameof(Package)}");
            }
        }
    }
}
