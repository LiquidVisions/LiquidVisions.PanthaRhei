using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal class InitializeComponentsUseCase : IInitializeComponentsUseCase
    {
        private readonly IGenericRepository<Component> repository;
        private readonly Parameters parameters;
        private readonly IDirectoryService directoryService;
        private readonly IFileService fileService;

        public InitializeComponentsUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<Component>>();
            parameters = dependencyResolver.Get<Parameters>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
            fileService = dependencyResolver.Get<IFileService>();
        }

        public IEnumerable<Component> Initialize(IEnumerable<Expander> expanders)
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
                            string componentName = fileName.Replace("NAME.", string.Empty);

                            Component component = new()
                            {
                                Id = Guid.NewGuid(),
                                Name = componentName,
                                Expander = expander,
                            };

                            repository.Create(component);

                            yield return component;
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
