using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Seeders
{
    internal class SeederInteractor : ISeederInteractor
    {
        private readonly GenerationOptions options;
        private readonly List<IEntitySeederInteractor<App>> seeders;

        public SeederInteractor(IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            seeders = dependencyFactory.GetAll<IEntitySeederInteractor<App>>().ToList();
        }

        public bool CanExecute => options.ReSeed;

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
