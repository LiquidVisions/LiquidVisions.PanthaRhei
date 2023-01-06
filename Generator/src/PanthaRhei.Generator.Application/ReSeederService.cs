using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders;

namespace LiquidVisions.PanthaRhei.Generator.Application
{
    internal class ReSeederService : IReSeederService
    {
        private readonly IEnumerable<ISeeder<App>> seeders;

        public ReSeederService(IDependencyResolver dependencyResolver)
        {
            seeders = dependencyResolver.GetAll<ISeeder<App>>();
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
