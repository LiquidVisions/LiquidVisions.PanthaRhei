using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class Seeder : ISeeder
    {
        private readonly GenerationOptions _options;
        private readonly List<IEntitySeeder<App>> _seeders;

        public Seeder(IDependencyFactory dependencyFactory)
        {
            _options = dependencyFactory.Resolve<GenerationOptions>();
            _seeders = dependencyFactory.ResolveAll<IEntitySeeder<App>>().ToList();
        }

        public bool Enabled => _options.Seed;

        public void Execute()
        {
            App app = new();

            _seeders.OrderBy(x => x.ResetOrder)
                .ToList()
                .ForEach(x => x.Reset());

            _seeders.OrderBy(x => x.SeedOrder)
                .ToList()
                .ForEach(x => x.Seed(app));
        }
    }
}
