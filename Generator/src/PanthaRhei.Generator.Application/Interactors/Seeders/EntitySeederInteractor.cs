﻿using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class EntitySeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericGateway<Entity> gateway;

        public EntitySeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGenericGateway<Entity>>();
        }

        public int SeedOrder => 5;

        public int ResetOrder => 5;

        public void Reset() => gateway.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> all = gateway.ContextType.GetProperties()
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

                gateway.Create(entity);
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
