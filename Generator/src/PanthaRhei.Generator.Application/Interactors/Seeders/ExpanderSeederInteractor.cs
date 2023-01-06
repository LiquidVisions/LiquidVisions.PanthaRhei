using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class ExpanderSeederInteractor : ISeederInteractor<App>
    {
        private readonly IExpanderPluginLoaderInteractor pluginLoader;
        private readonly Parameters parameters;
        private readonly IGenericGateway<Expander> expanderRepository;

        public ExpanderSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoaderInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            expanderRepository = dependencyFactory.Get<IGenericGateway<Expander>>();
        }

        public int SeedOrder => 2;

        public int ResetOrder => 4;

        public void Seed(App app)
        {
            List<IExpanderInteractor> expanders = pluginLoader.ShallowLoadAllExpanders(parameters.ExpandersFolder);
            foreach (IExpanderInteractor exp in expanders)
            {
                Expander expander = new()
                {
                    Id = Guid.NewGuid(),
                    Name = exp.Name,
                    Order = exp.Order,
                    TemplateFolder = ".Templates",
                };

                expander.Apps.Add(app);
                app.Expanders.Add(expander);

                expanderRepository.Create(expander);
            }
        }

        public void Reset() => expanderRepository.DeleteAll();
    }
}
