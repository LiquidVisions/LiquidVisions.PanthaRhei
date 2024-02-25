using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class Seeder(IDependencyFactory dependencyFactory) : ISeeder
    {
        private readonly GenerationOptions options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly IEnumerable<IEntitySeeder<App>> seeders = dependencyFactory.ResolveAll<IEntitySeeder<App>>();

        public bool Enabled => options.Seed;

        public void Execute()
        {
            App app = new();

            seeders.OrderBy(x => x.ResetOrder)
                .ToList()
                .ForEach(x => x.Reset());

            seeders.OrderBy(x => x.SeedOrder)
                .ToList()
                .ForEach(x => x.Seed(app));
        }
    }
}
