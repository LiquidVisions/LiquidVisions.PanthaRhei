using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class AppSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly ICreateRepository<App> _createGateway = dependencyFactory.Resolve<ICreateRepository<App>>();
        private readonly IDeleteRepository<App> _deleteGateway = dependencyFactory.Resolve<IDeleteRepository<App>>();
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();

        public int SeedOrder => 1;

        public int ResetOrder => 1;

        public void Seed(App app)
        {
            app.Id = _options.AppId;
            app.Name = "PanthaRhei.Generated";
            app.FullName = "LiquidVisions.PanthaRhei.Generated";

            _createGateway.Create(app);
        }

        public void Reset() => _deleteGateway.DeleteAll();
    }
}
