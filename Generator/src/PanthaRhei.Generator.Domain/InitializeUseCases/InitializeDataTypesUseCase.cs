using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.DataInitializers
{
    internal class InitializeDataTypesUseCase : IInitializeDataTypesUseCase
    {
        private readonly IGenericRepository<DataType> repository;

        public InitializeDataTypesUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<DataType>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidProgramException("Failed to delete all datatypes.");
            }
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
