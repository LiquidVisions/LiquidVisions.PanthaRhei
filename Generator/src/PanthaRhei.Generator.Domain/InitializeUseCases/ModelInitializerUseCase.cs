﻿using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases;
using LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.DataInitializers
{
    public class ModelInitializerUseCase : IModelInitializerUseCase
    {
        private readonly IInitializeAppUseCase appInitializer;
        private readonly IInitializeExpandersUseCase expanderInitializer;
        private readonly IInitializeComponentsUseCase componentInitializer;
        private readonly IInitializeHandlerUseCase handlerInitializer;
        private readonly IInitializePackagesUseCase packagesUseCase;

        private readonly IInitializeDataTypesUseCase dataTypesUseCase;
        private readonly IInitializeFieldsUseCase fieldsUseCase;
        private readonly IInitializeEntitiesUseCase entitiesUseCase;


        public ModelInitializerUseCase(IDependencyResolver dependencyResolver)
        {
            appInitializer = dependencyResolver.Get<IInitializeAppUseCase>();
            expanderInitializer = dependencyResolver.Get<IInitializeExpandersUseCase>();
            componentInitializer = dependencyResolver.Get<IInitializeComponentsUseCase>();
            handlerInitializer = dependencyResolver.Get<IInitializeHandlerUseCase>();
            packagesUseCase = dependencyResolver.Get<IInitializePackagesUseCase>();
            dataTypesUseCase = dependencyResolver.Get<IInitializeDataTypesUseCase>();
            fieldsUseCase = dependencyResolver.Get<IInitializeFieldsUseCase>();
            entitiesUseCase = dependencyResolver.Get<IInitializeEntitiesUseCase>();
        }

        public void Initialize()
        {
            // clean things up.
            appInitializer.DeleteAll();
            packagesUseCase.DeleteAll();
            handlerInitializer.DeleteAll();
            expanderInitializer.DeleteAll();
            dataTypesUseCase.DeleteAll();
            fieldsUseCase.DeleteAll();
            entitiesUseCase.DeleteAll();

            // reinitialize
            App app = appInitializer.Initialize();
            expanderInitializer.Initialize(app);
            componentInitializer.Initialize(app.Expanders);
        }
    }
}
