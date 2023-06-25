using System;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Seeders
{
    internal class ConnectionStringsSeederInteractor : IEntitySeederInteractor<App>
    {
        private readonly ICreateGateway<ConnectionString> createGateway;
        private readonly IDeleteGateway<ConnectionString> deleteGateway;

        public ConnectionStringsSeederInteractor(IDependencyFactory dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateGateway<ConnectionString>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<ConnectionString>>();
        }

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            ConnectionString connectionString = new()
            {
                Id = Guid.NewGuid(),
                Name = Resources.ConnectionStringName,
                Definition = Resources.ConnectionStringDefintion,
            };

            app.ConnectionStrings.Add(connectionString);
            connectionString.App = app;

            createGateway.Create(connectionString);
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
