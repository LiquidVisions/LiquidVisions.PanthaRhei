using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders
{
    internal class AppSeeder : ISeeder<App>
    {
        private readonly IGenericRepository<App> repository;
        private readonly Parameters parameters;

        public AppSeeder(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<App>>();
            parameters = dependencyResolver.Get<Parameters>();
        }

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            app.Id = parameters.AppId;
            app.Name = "PanthaRhei.Generated";
            app.FullName = "LiquidVisions.PanthaRhei.Generated";

            repository.Create(app);
        }

        public void Reset() => repository.DeleteAll();
    }
}
