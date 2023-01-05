using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    internal class InitializeAppUseCase : IInitializeAppUseCase
    {
        private readonly IGenericRepository<App> repository;
        private readonly Parameters parameters;

        public InitializeAppUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<App>>();
            parameters = dependencyResolver.Get<Parameters>();
        }

        public App Initialize()
        {
            App app = new()
            {
                Id = parameters.AppId,
                Name = "PanthaRhei",
                FullName = "LiquidVisions.PanthaRhei",
            };

            if (!repository.Create(app))
            {
                throw new InvalidProgramException("App was not created properly");
            }

            return app;
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidProgramException("Failed to delete all apps.");
            }
        }
    }
}
