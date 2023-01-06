using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Initializers;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal class InitializeExpandersUseCase : IInitializeExpandersUseCase
    {
        private readonly IExpanderPluginLoader pluginLoader;
        private readonly Parameters parameters;
        private readonly IGenericRepository<Expander> expanderRepository;

        public InitializeExpandersUseCase(IDependencyResolver dependencyResolver)
        {
            pluginLoader = dependencyResolver.Get<IExpanderPluginLoader>();
            parameters = dependencyResolver.Get<Parameters>();
            expanderRepository = dependencyResolver.Get<IGenericRepository<Expander>>();
        }

        public void Initialize(App app)
        {
            List<IExpander> expanders = pluginLoader.ShallowLoadAllExpanders(parameters.ExpandersFolder);
            foreach (IExpander expander in expanders)
            {
                CreateApp(app, expander);
            }
        }

        public void DeleteAll()
        {
            if (!expanderRepository.DeleteAll())
            {
                throw new InvalidOperationException($"Failed to delete all the {nameof(Expander)}");
            }
        }

        private void CreateApp(App app, IExpander runtimeExpander)
        {
            Expander expander = new()
            {
                Id = Guid.NewGuid(),
                Name = runtimeExpander.Name,
                Order = runtimeExpander.Order,
                TemplateFolder = ".Templates",
            };

            expander.Apps.Add(app);
            app.Expanders.Add(expander);

            SaveExpanderToDatabase(expander);
        }

        private void SaveExpanderToDatabase(Expander expander)
        {
            bool succeeded = expanderRepository.Create(expander);
            if (!succeeded)
            {
                throw new InvalidOperationException($"Failed to add {expander.Name} to the database.");
            }
        }
    }
}
