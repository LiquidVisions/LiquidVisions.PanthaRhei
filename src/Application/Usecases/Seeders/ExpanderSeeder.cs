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
        private readonly IExpanderPluginLoader _pluginLoader = dependencyFactory.Resolve<IExpanderPluginLoader>();
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly ICreateRepository<Expander> _createGateway = dependencyFactory.Resolve<ICreateRepository<Expander>>();
        private readonly IDeleteRepository<Expander> _deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Expander>>();

        public int SeedOrder => 2;

        public int ResetOrder => 4;

        public void Seed(App app)
        {
            foreach (IExpander exp in _pluginLoader.ShallowLoadAllExpanders(_options.ExpandersFolder))
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

                _createGateway.Create(expander);
            }
        }

        public void Reset() => _deleteGateway.DeleteAll();
    }
}
