using System;
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
    public class PackageSeeder : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Package> createGateway;
        private readonly IDeleteRepository<Package> deleteGateway;
        private readonly IDirectory directoryService;
        private readonly GenerationOptions options;

        public PackageSeeder(IDependencyFactory dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateRepository<Package>>();
            deleteGateway = dependencyFactory.Get<IDeleteRepository<Package>>();
            directoryService = dependencyFactory.Get<IDirectory>();
            options = dependencyFactory.Get<GenerationOptions>();
        }

        public int SeedOrder => 4;

        public int ResetOrder => 2;

        public void Reset() => deleteGateway.DeleteAll();

        public void Seed(App app)
        {
            foreach (Component component in app.Expanders.SelectMany(x => x.Components))
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
                XDocument xml = XDocument.Load(csproj);
                var packageReferenceElements = xml.Descendants("PackageReference");
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

            createGateway.Create(package);
        }
    }
}
