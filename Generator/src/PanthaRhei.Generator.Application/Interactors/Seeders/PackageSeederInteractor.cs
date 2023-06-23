using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    public class PackageSeederInteractor : IEntitySeederInteractor<App>
    {
        private readonly ICreateGateway<Package> createGateway;
        private readonly IDeleteGateway<Package> deleteGateway;
        private readonly IDirectory directoryService;
        private readonly GenerationOptions options;

        public PackageSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateGateway<Package>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<Package>>();
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
                string templatePath = Path.Combine(options.ExpandersFolder, component.Expander.Name, component.Expander.TemplateFolder);
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
