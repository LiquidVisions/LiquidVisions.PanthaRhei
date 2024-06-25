using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class AppSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly ICreateRepository<App> createGateway = dependencyFactory.Resolve<ICreateRepository<App>>();
        private readonly IDeleteRepository<App> deleteGateway = dependencyFactory.Resolve<IDeleteRepository<App>>();
        private readonly GenerationOptions options = dependencyFactory.Resolve<GenerationOptions>();

        public int SeedOrder => 1;

        public int ResetOrder => 12;

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
