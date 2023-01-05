using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    public class InitializeHandlerUseCase : IInitializeHandlerUseCase
    {
        private readonly IGenericRepository<Handler> repository;

        public InitializeHandlerUseCase(IDependencyResolver dependencyResolver)
        {
            this.repository = dependencyResolver.Get<IGenericRepository<Handler>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidOperationException($"Failed to delete all the {nameof(Handler)}");
            }
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
