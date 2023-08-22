using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class ExpanderSeeder : IEntitySeeder<App>
    {
        private readonly IExpanderPluginLoader pluginLoader;
        private readonly GenerationOptions options;
        private readonly ICreateRepository<Expander> createGateway;
        private readonly IDeleteRepository<Expander> deleteGateway;

        public ExpanderSeeder(IDependencyFactory dependencyFactory)
        {
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoader>();
            options = dependencyFactory.Get<GenerationOptions>();
            createGateway = dependencyFactory.Get<ICreateRepository<Expander>>();
            deleteGateway = dependencyFactory.Get<IDeleteRepository<Expander>>();
        }

        public int SeedOrder => 2;

        public int ResetOrder => 4;

        public void Seed(App app)
        {
            List<IExpander> expanders = pluginLoader.ShallowLoadAllExpanders(options.ExpandersFolder);
            foreach (IExpander exp in expanders)
            {
                Expander expander = new()
                {
                    Id = Guid.NewGuid(),
                    Name = exp.Name,
                    Order = exp.Order,
                    Enabled = true,
                };

                expander.Apps.Add(app);
                app.Expanders.Add(expander);

                createGateway.Create(expander);
            }
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
