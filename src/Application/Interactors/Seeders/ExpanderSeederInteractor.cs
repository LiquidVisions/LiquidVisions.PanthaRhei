using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Seeders
{
    internal class ExpanderSeederInteractor : IEntitySeederInteractor<App>
    {
        private readonly IExpanderPluginLoaderInteractor pluginLoader;
        private readonly GenerationOptions options;
        private readonly ICreateGateway<Expander> createGateway;
        private readonly IDeleteGateway<Expander> deleteGateway;

        public ExpanderSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoaderInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            createGateway = dependencyFactory.Get<ICreateGateway<Expander>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<Expander>>();
        }

        public int SeedOrder => 2;

        public int ResetOrder => 4;

        public void Seed(App app)
        {
            List<IExpanderInteractor> expanders = pluginLoader.ShallowLoadAllExpanders(options.ExpandersFolder);
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

                createGateway.Create(expander);
            }
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
