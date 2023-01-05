using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.DataInitializers
{
    internal class InitializeFieldsUseCase : IInitializeFieldsUseCase
    {
        private readonly IGenericRepository<Field> repository;

        public InitializeFieldsUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<Field>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidProgramException("Failed to delete all fields.");
            }
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
