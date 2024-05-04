using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class ConnectionStringsSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly ICreateRepository<ConnectionString> createGateway = dependencyFactory.Resolve<ICreateRepository<ConnectionString>>();
        private readonly IDeleteRepository<ConnectionString> deleteGateway = dependencyFactory.Resolve<IDeleteRepository<ConnectionString>>();

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
