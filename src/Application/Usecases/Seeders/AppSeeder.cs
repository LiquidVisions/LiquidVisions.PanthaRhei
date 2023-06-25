using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class AppSeeder : IEntitySeeder<App>
    {
        private readonly ICreateRepository<App> createGateway;
        private readonly IDeleteRepository<App> deleteGateway;
        private readonly GenerationOptions options;

        public AppSeeder(IDependencyFactory dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateRepository<App>>();
            deleteGateway = dependencyFactory.Get<IDeleteRepository<App>>();
            options = dependencyFactory.Get<GenerationOptions>();
        }

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            app.Id = options.AppId;
            app.Name = "PanthaRhei.Generated";
            app.FullName = "LiquidVisions.PanthaRhei.Generated";

            createGateway.Create(app);
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
