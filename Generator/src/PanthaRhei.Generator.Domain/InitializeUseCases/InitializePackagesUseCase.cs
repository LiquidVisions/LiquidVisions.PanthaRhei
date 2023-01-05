using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    public class InitializePackagesUseCase : IInitializePackagesUseCase
    {
        private readonly IGenericRepository<Package> repository;
        private readonly IDirectoryService directoryService;
        private readonly Parameters parameters;

        public InitializePackagesUseCase(IDependencyResolver dependencyResolver)
        {
            this.repository = dependencyResolver.Get<IGenericRepository<Package>>();
            this.directoryService = dependencyResolver.Get<IDirectoryService>();
            this.parameters = dependencyResolver.Get<Parameters>();

        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidOperationException($"Failed to delete all the {nameof(Package)}");
            }
        }

        public void Initialize(IEnumerable<Component> components)
        {
            foreach (Component component in components)
            {
                HandleComponent(component);
            }
        }

        private void HandleComponent(Component component)
        {
            Expander expander = component.Expander;

            string templatePath = Path.Combine(parameters.ExpandersFolder, expander.Name, expander.TemplateFolder);
            if (directoryService.Exists(templatePath))
            {
                HandleTemplate(component, templatePath);
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

            repository.Create(package);
        }
    }
}
