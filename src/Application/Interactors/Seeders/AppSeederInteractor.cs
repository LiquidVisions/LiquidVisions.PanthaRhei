using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Seeders
{
    internal class AppSeederInteractor : IEntitySeederInteractor<App>
    {
        private readonly ICreateGateway<App> createGateway;
        private readonly IDeleteGateway<App> deleteGateway;
        private readonly GenerationOptions options;

        public AppSeederInteractor(IDependencyFactory dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateGateway<App>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<App>>();
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
