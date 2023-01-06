using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal class InitializeEntitiesUseCase : IInitializeEntitiesUseCase
    {
        private readonly IGenericRepository<Entity> repository;

        public InitializeEntitiesUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<Entity>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidProgramException("Failed to delete all entities.");
            }
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
