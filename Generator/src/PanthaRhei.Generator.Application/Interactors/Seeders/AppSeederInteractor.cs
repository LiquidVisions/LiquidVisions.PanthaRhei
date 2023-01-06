using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class AppSeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericGateway<App> gateway;
        private readonly Parameters parameters;

        public AppSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGenericGateway<App>>();
            parameters = dependencyFactory.Get<Parameters>();
        }

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            app.Id = parameters.AppId;
            app.Name = "PanthaRhei.Generated";
            app.FullName = "LiquidVisions.PanthaRhei.Generated";

            gateway.Create(app);
        }

        public void Reset() => gateway.DeleteAll();
    }
}
