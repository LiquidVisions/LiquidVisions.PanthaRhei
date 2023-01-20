using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class EntitySeederInteractor : ISeederInteractor<App>
    {
        private readonly ICreateGateway<Entity> createGateway;
        private readonly IDeleteGateway<Entity> deleteGateway;
        private readonly IEntitiesToSeedGateway entitySeederGateway;

        public EntitySeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateGateway<Entity>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<Entity>>();
            entitySeederGateway = dependencyFactory.Get<IEntitiesToSeedGateway>();
        }

        public int SeedOrder => 5;

        public int ResetOrder => 5;

        public void Reset() => deleteGateway.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> all = entitySeederGateway.GetAll();

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

                createGateway.Create(entity);
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
