using LiquidVisions.PanthaRhei.Generator.Domain.DataInitializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application
{
    internal class ReSeederService : IReSeederService
    {
        private readonly IModelInitializerUseCase initializer;

        public ReSeederService(IDependencyResolver dependencyResolver)
        {
            initializer = dependencyResolver.Get<IModelInitializerUseCase>();
        }

        public void Execute() 
            => initializer.Initialize();
    }
}
