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
    internal class ComponentSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Component> _createGateway = dependencyFactory.Resolve<ICreateRepository<Component>>();
        private readonly IDeleteRepository<Component> _deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Component>>();
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly IDirectory _directoryService = dependencyFactory.Resolve<IDirectory>();
        private readonly IFile _fileService = dependencyFactory.Resolve<IFile>();

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

                    ArgumentNullException.ThrowIfNull(files);

                    foreach (string file in files)
                    {
                        string fileName = _fileService.GetFileNameWithoutExtension(file);
                        string componentName = fileName.Replace("NAME.", string.Empty, StringComparison.InvariantCulture);

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

        public void Reset() => _deleteGateway.DeleteAll();
    }
}
