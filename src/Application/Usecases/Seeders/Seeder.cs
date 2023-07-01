﻿using System.Collections.Generic;
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

        public bool Enabled => options.ReSeed;

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