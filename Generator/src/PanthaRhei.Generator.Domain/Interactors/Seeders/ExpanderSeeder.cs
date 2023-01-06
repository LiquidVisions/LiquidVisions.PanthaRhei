using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders
{
    internal class ExpanderSeeder : ISeeder<App>
    {
        private readonly IExpanderPluginLoader pluginLoader;
        private readonly Parameters parameters;
        private readonly IGenericRepository<Expander> expanderRepository;

        public ExpanderSeeder(IDependencyFactoryInteractor dependencyFactory)
        {
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoader>();
            parameters = dependencyFactory.Get<Parameters>();
            expanderRepository = dependencyFactory.Get<IGenericRepository<Expander>>();
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
