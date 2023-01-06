using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders
{
    internal class FieldSeeder : ISeeder<App>
    {
        private readonly IGenericRepository<Field> repository;

        public FieldSeeder(IDependencyFactoryInteractor dependencyFactory)
        {
            repository = dependencyFactory.Get<IGenericRepository<Field>>();
        }

        public int SeedOrder => 6;

        public int ResetOrder => 6;

        public void Reset() => repository.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> allEntities = repository.ContextType.GetProperties()
                .Where(x => x.PropertyType.Name == "DbSet`1")
                .Select(x => x.PropertyType.GetGenericArguments().First());

            foreach (Type entityType in allEntities)
            {
                IEnumerable<PropertyInfo> allProperties = entityType.GetProperties();
                int order = 1;
                foreach (PropertyInfo prop in allProperties)
                {
                    Field field = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = prop.Name,
                        IsCollection = GetIsCollection(prop),
                        Modifier = GetModifier(prop),
                        GetModifier = GetModifier(prop.GetMethod),
                        SetModifier = GetModifier(prop.SetMethod),
                        Behaviour = GetBehaviour(prop),
                        Order = order++,
                    };

                    SetReturnType(prop, app, field);

                    Entity entity = app.Entities.Single(x => x.Name == entityType.Name);
                    entity.Fields.Add(field);

                    repository.Create(field);
                }
            }
        }

        private static bool GetIsCollection(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
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
