using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class ComponentSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Component> createGateway = dependencyFactory.Resolve<ICreateRepository<Component>>();
        private readonly IDeleteRepository<Component> deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Component>>();

        public int SeedOrder => 3;

        public int ResetOrder => 3;

        public void Seed(App app)
        {
            foreach (Expander expander in app.Expanders)
            {
                string[] split = expander.Name.Split('.');

                Component component = new()
                {
                    Id = Guid.NewGuid(),
                    Name = split.Length > 1 ? string.Join('.', split[1..^0]) : expander.Name,
                    Expander = expander,
                    App = app
                };

                createGateway.Create(component);
            }
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
