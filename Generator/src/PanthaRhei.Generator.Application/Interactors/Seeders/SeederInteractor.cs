using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class SeederInteractor : ISeederInteractor
    {
        private readonly ExpandRequestModel expandRequestModel;
        private readonly List<IEntitySeederInteractor<App>> seeders;

        public SeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            expandRequestModel = dependencyFactory.Get<ExpandRequestModel>();
            seeders = dependencyFactory.GetAll<IEntitySeederInteractor<App>>().ToList();
        }

        public bool CanExecute => expandRequestModel.ReSeed;

        public void Execute()
        {
            App app = new();

            foreach (var seeder in seeders.OrderBy(x => x.ResetOrder))
            {
                seeder.Reset();
            }

            foreach (var seeder in seeders.OrderBy(x => x.SeedOrder))
            {
                seeder.Seed(app);
            }
        }
    }
}
