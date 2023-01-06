using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
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
            //appInitializer.DeleteAll();
            //packagesUseCase.DeleteAll();
            //handlerInitializer.DeleteAll();
            //componentInitializer.DeleteAll();
            //expanderInitializer.DeleteAll();
            //dataTypesUseCase.DeleteAll();
            //fieldsUseCase.DeleteAll();
            //entitiesUseCase.DeleteAll();

            //// reinitialize
            //App app = appInitializer.Initialize();
            //expanderInitializer.Initialize(app);
            //var components = componentInitializer.Initialize(app.Expanders);
            //packagesUseCase.Initialize(components);

            dataTypesUseCase.Initialize();
            //entitiesUseCase.Initialize();
        }
    }
}
