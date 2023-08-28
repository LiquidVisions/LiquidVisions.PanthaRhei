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
        private readonly ICreateRepository<Component> _createGateway;
        private readonly IDeleteRepository<Component> _deleteGateway;
        private readonly GenerationOptions _options;
        private readonly IDirectory _directoryService;
        private readonly IFile _fileService;

        public ComponentSeeder(IDependencyFactory dependencyFactory)
        {
            _createGateway = dependencyFactory.Get<ICreateRepository<Component>>();
            _deleteGateway = dependencyFactory.Get<IDeleteRepository<Component>>();
            _options = dependencyFactory.Get<GenerationOptions>();
            _directoryService = dependencyFactory.Get<IDirectory>();
            _fileService = dependencyFactory.Get<IFile>();
        }

        public int SeedOrder => 3;

        public int ResetOrder => 3;

        public void Seed(App app)
        {
            foreach (Expander expander in app.Expanders)
            {
                string templatePath = Path.Combine(_options.ExpandersFolder, expander.Name, Resources.TemplatesFolder);
                if (_directoryService.Exists(templatePath))
                {
                    IEnumerable<string> files = _directoryService.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories)
                        .Where(x => !string.IsNullOrEmpty(x));

                    if (files != null && files.Any())
                    {
                        foreach (string file in files)
                        {
                            string fileName = _fileService.GetFileNameWithoutExtension(file);
                            string componentName = fileName.Replace("NAME.", string.Empty);

                            Component component = new()
                            {
                                Id = Guid.NewGuid(),
                                Name = componentName,
                                Expander = expander,
                            };

                            _createGateway.Create(component);
                        }
                    }
                }
            }
        }

        public void Reset() => _deleteGateway.DeleteAll();
    }
}
