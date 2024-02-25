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
        private readonly ICreateRepository<Package> createGateway;
        private readonly IDeleteRepository<Package> deleteGateway;
        private readonly IXDocument xDocument;
        private readonly IDirectory directoryService;
        private readonly GenerationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageSeeder"/> class.
        /// </summary>
        /// <param name="dependencyFactory"></param>
        public PackageSeeder(IDependencyFactory dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory);

            createGateway = dependencyFactory.Resolve<ICreateRepository<Package>>();
            deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Package>>();
            directoryService = dependencyFactory.Resolve<IDirectory>();
            xDocument = dependencyFactory.Resolve<IXDocument>();
            options = dependencyFactory.Resolve<GenerationOptions>();
        }

        /// <inheritdoc/>
        public int SeedOrder => 4;

        /// <inheritdoc/>
        public int ResetOrder => 2;

        /// <inheritdoc/>
        public void Reset() => deleteGateway.DeleteAll();

        /// <summary>
        /// Seeds the <see cref="Package"/> entity.
        /// </summary>
        /// <param name="entity"><seealso cref="App"/></param>
        public void Seed(App entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            foreach (Component component in entity.Expanders.SelectMany(x => x.Components))
            {
                string templatePath = Path.Combine(options.ExpandersFolder, component.Expander.Name, Resources.TemplatesFolder);
                if (directoryService.Exists(templatePath))
                {
                    HandleTemplate(component, templatePath);
                }
            }
        }

        private void HandleTemplate(Component component, string templatePath)
        {
            string[] files = directoryService.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories);
            foreach (string csproj in files)
            {
                XDocument xml = xDocument.Load(csproj);
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
                Name = packageReferenceElement.Attribute("Update").Value,
                Version = packageReferenceElement.Attribute("Version").Value,
                Component = component,
            };

            component.Packages.Add(package);

            createGateway.Create(package);
        }
    }
}
