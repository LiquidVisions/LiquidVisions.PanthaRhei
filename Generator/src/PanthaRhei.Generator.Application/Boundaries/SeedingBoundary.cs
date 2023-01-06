using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders;

namespace LiquidVisions.PanthaRhei.Generator.Application.Boundaries
{
    internal class SeedingBoundary : ISeedingBoundary
    {
        private readonly IEnumerable<ISeeder<App>> seeders;

        public SeedingBoundary(IDependencyFactoryInteractor dependencyFactory)
        {
            seeders = dependencyFactory.GetAll<ISeeder<App>>();
        }

        public void Execute()
        {
            foreach (var seeder in seeders.OrderBy(x => x.ResetOrder))
            {
                seeder.Reset();
            }

            App app = new();
            foreach (var seeder in seeders.OrderBy(x => x.SeedOrder))
            {
                seeder.Seed(app);
            }

        }
    }
}
