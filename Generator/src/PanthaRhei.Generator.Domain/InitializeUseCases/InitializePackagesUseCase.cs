using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    public class InitializePackagesUseCase : IInitializePackagesUseCase
    {
        private readonly IGenericRepository<Package> repository;

        public InitializePackagesUseCase(IDependencyResolver dependencyResolver)
        {
            this.repository = dependencyResolver.Get<IGenericRepository<Package>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidOperationException($"Failed to delete all the {nameof(Package)}");
            }
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
