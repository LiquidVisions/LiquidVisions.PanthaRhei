using System;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class ComponentSeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericRepository<Component> repository;
        private readonly Parameters parameters;
        private readonly IDirectory directoryService;
        private readonly IFile fileService;

        public ComponentSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            repository = dependencyFactory.Get<IGenericRepository<Component>>();
            parameters = dependencyFactory.Get<Parameters>();
            directoryService = dependencyFactory.Get<IDirectory>();
            fileService = dependencyFactory.Get<IFile>();
        }

        public int SeedOrder => 3;

        public int ResetOrder => 3;

        public void Seed(App app)
        {
            foreach (Expander expander in app.Expanders)
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
                        }
                    }
                }
            }
        }

        public void Reset() => repository.DeleteAll();
    }
}
