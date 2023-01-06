using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders
{
    internal class EntitySeeder : ISeeder<App>
    {
        private readonly IGenericRepository<Entity> repository;

        public EntitySeeder(IDependencyFactoryInteractor dependencyFactory)
        {
            repository = dependencyFactory.Get<IGenericRepository<Entity>>();
        }

        public int SeedOrder => 5;

        public int ResetOrder => 5;

        public void Reset() => repository.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> all = repository.ContextType.GetProperties()
                .Where(x => x.PropertyType.Name == "DbSet`1")
                .Select(x => x.PropertyType.GetGenericArguments().First());

            foreach (Type type in all)
            {
                var entity = new Entity
                {
                    Id = Guid.NewGuid(),
                    Name = type.Name,
                    Callsite = $"{app.FullName}.Domain.Entities",
                    Type = GetType(type),
                    Modifier = GetModifier(type),
                    Behaviour = GetBehaviour(type),
                    App = app,
                };
                app.Entities.Add(entity);

                repository.Create(entity);
            }
        }

        private static string GetModifier(Type type)
        {
            if (type.IsPublic)
            {
                return "public";
            }

            return "private";
        }

        private static string GetBehaviour(Type type)
        {
            if (type.IsAbstract)
            {
                return "abstract";
            }

            return null;
        }

        private static string GetType(Type type)
        {
            if (type.IsInterface)
            {
                return "interface";
            }

            if (type.IsClass)
            {
                return "class";
            }

            if (type.IsEnum)
            {
                return "enum";
            }

            throw new NotImplementedException();
        }
    }
}
