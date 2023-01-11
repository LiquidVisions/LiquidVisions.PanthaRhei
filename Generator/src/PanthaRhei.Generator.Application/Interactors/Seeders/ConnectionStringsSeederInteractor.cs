﻿using System;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class ConnectionStringsSeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericGateway<ConnectionString> gateway;

        public ConnectionStringsSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGenericGateway<ConnectionString>>();
        }

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            ConnectionString connectionString = new()
            {
                Id = Guid.NewGuid(),
                Name = "DefaultConnectionString",
                Definition = "Server=tcp:liquidvisions.database.windows.net,1433;Initial Catalog=PantaRhei.Prod;Persist Security Info=False;User ID=gerco.koks;Password=4cZ#Lsojpc75;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
            };

            app.ConnectionStrings.Add(connectionString);
            connectionString.App = app;

            gateway.Create(connectionString);
        }

        public void Reset() => gateway.DeleteAll();
    }
}