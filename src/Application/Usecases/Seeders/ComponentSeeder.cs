using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class ComponentSeeder : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Component> createGateway;
        private readonly IDeleteRepository<Component> deleteGateway;
        private readonly GenerationOptions options;
        private readonly IDirectory directoryService;
        private readonly IFile fileService;

        public ComponentSeeder(IDependencyFactory dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateRepository<Component>>();
            deleteGateway = dependencyFactory.Get<IDeleteRepository<Component>>();
            options = dependencyFactory.Get<GenerationOptions>();
            directoryService = dependencyFactory.Get<IDirectory>();
            fileService = dependencyFactory.Get<IFile>();
        }

        public int SeedOrder => 3;

        public int ResetOrder => 3;

        public void Seed(App app)
        {
            foreach (Expander expander in app.Expanders)
            {
                string templatePath = Path.Combine(options.ExpandersFolder, expander.Name, expander.TemplateFolder);
                if (directoryService.Exists(templatePath))
                {
                    IEnumerable<string> files = directoryService.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories)
                        .Where(x => !string.IsNullOrEmpty(x));

                    if (files != null && files.Any())
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

                            createGateway.Create(component);
                        }
                    }
                }
            }
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
