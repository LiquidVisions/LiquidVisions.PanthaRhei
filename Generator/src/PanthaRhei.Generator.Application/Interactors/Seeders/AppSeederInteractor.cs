﻿using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class AppSeederInteractor : IEntitySeederInteractor<App>
    {
        private readonly ICreateGateway<App> createGateway;
        private readonly IDeleteGateway<App> deleteGateway;
        private readonly ExpandRequestModel expandRequestModel;

        public AppSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateGateway<App>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<App>>();
            expandRequestModel = dependencyFactory.Get<ExpandRequestModel>();
        }

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            app.Id = expandRequestModel.AppId;
            app.Name = "PanthaRhei.Generated";
            app.FullName = "LiquidVisions.PanthaRhei.Generated";

            createGateway.Create(app);
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
