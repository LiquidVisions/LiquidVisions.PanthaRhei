using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    /// <summary>
    /// Seeds the <see cref="Package"/> entity.
    /// </summary>
    public class PackageSeeder : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Package> _createGateway;
        private readonly IDeleteRepository<Package> _deleteGateway;
        private readonly IDirectory _directoryService;
        private readonly GenerationOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageSeeder"/> class.
        /// </summary>
        /// <param name="dependencyFactory"></param>
        public PackageSeeder(IDependencyFactory dependencyFactory)
        {
            _createGateway = dependencyFactory.Get<ICreateRepository<Package>>();
            _deleteGateway = dependencyFactory.Get<IDeleteRepository<Package>>();
            _directoryService = dependencyFactory.Get<IDirectory>();
            _options = dependencyFactory.Get<GenerationOptions>();
        }

        /// <inheritdoc/>
        public int SeedOrder => 4;

        /// <inheritdoc/>
        public int ResetOrder => 2;

        /// <inheritdoc/>
        public void Reset() => _deleteGateway.DeleteAll();

        /// <summary>
        /// Seeds the <see cref="Package"/> entity.
        /// </summary>
        /// <param name="app"><seealso cref="App"/></param>
        public void Seed(App app)
        {
            foreach (Component component in app.Expanders.SelectMany(x => x.Components))
            {
                string templatePath = Path.Combine(_options.ExpandersFolder, component.Expander.Name, Resources.TemplatesFolder);
                if (_directoryService.Exists(templatePath))
                {
                    HandleTemplate(component, templatePath);
                }
            }
        }

        private void HandleTemplate(Component component, string templatePath)
        {
            string[] files = _directoryService.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories);
            foreach (string csproj in files)
            {
                XDocument xml = XDocument.Load(csproj);
                IEnumerable<XElement> packageReferenceElements = xml.Descendants("PackageReference");
                foreach (XElement packageReferenceElement in packageReferenceElements)
                {
                    HandlePackage(component, packageReferenceElement);
                }
            }
        }

        private void HandlePackage(Component component, XElement packageReferenceElement)
        {
            Package package = new()
            {
                Id = Guid.NewGuid(),
                Name = packageReferenceElement.Attribute("Include").Value,
                Version = packageReferenceElement.Attribute("Version").Value,
                Component = component,
            };

            component.Packages.Add(package);

            _createGateway.Create(package);
        }
    }
}
