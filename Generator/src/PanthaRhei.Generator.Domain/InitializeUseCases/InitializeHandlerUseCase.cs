using System;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    public class InitializeHandlerUseCase : IInitializeHandlerUseCase
    {
        private readonly IGenericRepository<Handler> repository;

        public InitializeHandlerUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<Handler>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidOperationException($"Failed to delete all the {nameof(Handler)}");
            }
        }

        public void Initialize(App app)
        {
            foreach (Expander expander in app.Expanders)
            {
                AssemblyName expanderAssembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Single(x => x.Name.Contains(expander.Name));
            }
        }
    }
}
