using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal class InitializeEntitiesUseCase : IInitializeEntitiesUseCase
    {
        private readonly IGenericRepository<Entity> repository;

        public InitializeEntitiesUseCase(IDependencyResolver dependencyResolver)
        {
            repository = dependencyResolver.Get<IGenericRepository<Entity>>();
        }

        public void DeleteAll()
        {
            if (!repository.DeleteAll())
            {
                throw new InvalidProgramException("Failed to delete all entities.");
            }
        }

        public void Initialize(App app)
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
                    Callsite = type.Namespace,
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
            if(type.IsPublic)
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
