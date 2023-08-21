using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class Seeder : ISeeder
    {
        private readonly GenerationOptions options;
        private readonly List<IEntitySeeder<App>> seeders;

        public Seeder(IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            seeders = dependencyFactory.GetAll<IEntitySeeder<App>>().ToList();
        }

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
