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
    internal class ExpanderSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly IExpanderPluginLoader pluginLoader = dependencyFactory.Resolve<IExpanderPluginLoader>();
        private readonly GenerationOptions options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly ICreateRepository<Expander> createGateway = dependencyFactory.Resolve<ICreateRepository<Expander>>();
        private readonly IDeleteRepository<Expander> deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Expander>>();

        public int SeedOrder => 2;

        public int ResetOrder => 4;

        public void Seed(App app)
        {
            foreach (IExpander exp in pluginLoader.ShallowLoadAllExpanders(options.ExpandersFolder))
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
