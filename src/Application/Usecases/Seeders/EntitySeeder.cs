using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class EntitySeeder : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Entity> _createGateway;
        private readonly IDeleteRepository<Entity> _deleteGateway;
        private readonly IEntitiesToSeedRepository _entitySeederGateway;

        public EntitySeeder(IDependencyFactory dependencyFactory)
        {
            _createGateway = dependencyFactory.Resolve<ICreateRepository<Entity>>();
            _deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Entity>>();
            _entitySeederGateway = dependencyFactory.Resolve<IEntitiesToSeedRepository>();
        }

        public int SeedOrder => 5;

        public int ResetOrder => 5;

        public void Reset() => _deleteGateway.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> all = _entitySeederGateway.GetAll();

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

                _createGateway.Create(entity);
            }
        }

        private static string GetModifier(Type type)
        {
            if (type.IsPublic || type.IsNestedPublic)
            {
                return "public";
            }

            if (type.IsNotPublic || type.IsNestedPrivate)
            {
                return "private";
            }

            if (type.IsNested || type.IsNestedFamily)
            {
                return "protected";
            }

            throw new NotImplementedException();
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
