﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    internal class FieldSeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericGateway<Field> gateway;
        private readonly IModelConfiguration modelConfiguration;

        public FieldSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            this.gateway = dependencyFactory.Get<IGenericGateway<Field>>();
            modelConfiguration = dependencyFactory.Get<IModelConfiguration>();
        }

        public int SeedOrder => 6;

        public int ResetOrder => 6;

        public void Reset() => gateway.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> allEntities = gateway.ContextType.GetProperties()
                .Where(x => x.PropertyType.Name == "DbSet`1")
                .Select(x => x.PropertyType.GetGenericArguments().First());

            foreach (Type entityType in allEntities)
            {
                string[] keys = modelConfiguration.GetKeys(entityType);
                string[] indexes = modelConfiguration.GetIndexes(entityType);

                IEnumerable<PropertyInfo> allProperties = entityType.GetProperties();
                int order = 1;
                foreach (PropertyInfo prop in allProperties)
                {
                    int? size = modelConfiguration.GetSize(entityType, prop.Name);
                    bool required = modelConfiguration.GetIsRequired(entityType, prop.Name);

                    Field field = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = prop.Name,
                        IsCollection = GetIsCollection(prop),
                        Modifier = GetModifier(prop),
                        GetModifier = GetModifier(prop.GetMethod),
                        SetModifier = GetModifier(prop.SetMethod),
                        Behaviour = GetBehaviour(prop),
                        IsKey = keys.Any(x => x == prop.Name),
                        IsIndex = indexes.Any(x => x == prop.Name),
                        Order = order++,
                        Size = size,
                        Required = required,
                    };

                    SetReturnType(prop, app, field);

                    Entity entity = app.Entities.Single(x => x.Name == entityType.Name);
                    entity.Fields.Add(field);

                    gateway.Create(field);
                }
            }
        }

        private static bool GetIsCollection(PropertyInfo property)
        {
            bool isCollection = property.PropertyType.IsGenericType;
            isCollection &= property.PropertyType
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            return isCollection;
        }

        private static void SetReturnType(PropertyInfo prop, App app, Field field)
        {
            field.ReturnType = GetReturnType(prop);

            Entity entity = app.Entities
                .SingleOrDefault(x => x.Name == field.ReturnType);
            if (entity != null)
            {
                entity.ReferencedIn.Add(field);
                field.Reference = entity;
            }
        }

        private static string GetReturnType(PropertyInfo prop)
        {
            string returnType = prop.PropertyType.Name;
            if (prop.PropertyType.IsGenericType)
            {
                returnType = prop.PropertyType.GetGenericArguments().Single().Name;
            }

            switch (returnType)
            {
                case "String":
                    returnType = returnType.ToLower();
                    break;
                case "Int32":
                    returnType = "int";
                    break;
                case "Boolean":
                    returnType = "bool";
                    break;
                case "Decimal":
                    returnType = "decimal";
                    break;
                default:
                    break;
            }

            return returnType;
        }

        private static string GetModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
            {
                return "public";
            }

            if (methodInfo.IsPrivate)
            {
                return "private";
            }

            if (methodInfo.IsAssembly)
            {
                return "internal";
            }

            throw new NotImplementedException();
        }

        private static string GetModifier(PropertyInfo prop)
        {
            if (prop.GetMethod.IsPublic || prop.SetMethod.IsPublic)
            {
                return "public";
            }

            if (prop.GetMethod.IsPrivate && prop.SetMethod.IsPrivate)
            {
                return "private";
            }

            if (prop.GetMethod.IsAssembly || prop.SetMethod.IsAssembly)
            {
                return "internal";
            }

            throw new NotImplementedException();
        }

        private static string GetBehaviour(PropertyInfo prop)
        {
            if (prop.GetMethod.IsAbstract || prop.SetMethod.IsAbstract)
            {
                return "abstract";
            }

            if (prop.GetMethod.IsVirtual || prop.SetMethod.IsVirtual)
            {
                return "virtual";
            }

            return null;
        }
    }
}