﻿using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    public class ModelInitializerUseCase : IModelInitializerUseCase
    {
        private readonly IInitializeAppUseCase appInitializer;
        private readonly IInitializeExpandersUseCase expanderInitializer;
        private readonly IInitializeComponentsUseCase componentInitializer;
        private readonly IInitializePackagesUseCase packagesUseCase;

        private readonly IInitializeFieldsUseCase fieldsUseCase;
        private readonly IInitializeEntitiesUseCase entitiesUseCase;


        public ModelInitializerUseCase(IDependencyResolver dependencyResolver)
        {
            appInitializer = dependencyResolver.Get<IInitializeAppUseCase>();
            expanderInitializer = dependencyResolver.Get<IInitializeExpandersUseCase>();
            componentInitializer = dependencyResolver.Get<IInitializeComponentsUseCase>();
            packagesUseCase = dependencyResolver.Get<IInitializePackagesUseCase>();
            fieldsUseCase = dependencyResolver.Get<IInitializeFieldsUseCase>();
            entitiesUseCase = dependencyResolver.Get<IInitializeEntitiesUseCase>();
        }

        public void Initialize()
        {
            // clean things up.
            appInitializer.DeleteAll();
            packagesUseCase.DeleteAll();
            componentInitializer.DeleteAll();
            expanderInitializer.DeleteAll();
            fieldsUseCase.DeleteAll();
            entitiesUseCase.DeleteAll();

            // reinitialize
            App app = appInitializer.Initialize();
            expanderInitializer.Initialize(app);
            var components = componentInitializer.Initialize(app.Expanders);
            packagesUseCase.Initialize(components);

            entitiesUseCase.Initialize(app);
            fieldsUseCase.Initialize(app);
        }
    }
}
