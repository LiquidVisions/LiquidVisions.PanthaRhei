using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class FieldSeeder(IDependencyFactory dependencyFactory) : IEntitySeeder<App>
    {
        private readonly ICreateRepository<Field> _createGateway = dependencyFactory.Resolve<ICreateRepository<Field>>();
        private readonly IDeleteRepository<Field> _deleteGateway = dependencyFactory.Resolve<IDeleteRepository<Field>>();
        private readonly IEntitiesToSeedRepository _entitySeedGateway = dependencyFactory.Resolve<IEntitiesToSeedRepository>();
        private readonly IModelConfiguration _modelConfiguration = dependencyFactory.Resolve<IModelConfiguration>();

        public int SeedOrder => 6;

        public int ResetOrder => 6;

        public void Reset() => _deleteGateway.DeleteAll();

        public void Seed(App app)
        {
            IEnumerable<Type> allEntities = _entitySeedGateway.GetAll();
            foreach (Type entityType in allEntities)
            {
                IEnumerable<string> keys = _modelConfiguration.GetKeys(entityType);
                IEnumerable<string> indexes = _modelConfiguration.GetIndexes(entityType);

                IEnumerable<PropertyInfo> allProperties = entityType.GetProperties();
                int order = 1;
                foreach (PropertyInfo prop in allProperties)
                {
                    int? size = _modelConfiguration.GetSize(entityType, prop.Name);
                    bool required = _modelConfiguration.GetIsRequired(entityType, prop.Name);

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

                    _createGateway.Create(field);
                }
            }
        }

        private static bool GetIsCollection(PropertyInfo property)
        {
            bool isCollection = property.PropertyType.IsGenericType;
            isCollection &= property.PropertyType
                .GetInterfaces()
                .AsEnumerable()
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
                    returnType = "string";
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

            //if (methodInfo.IsAssembly)
            //{
            //    return "internal";
            //}

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

            //if (prop.GetMethod.IsAssembly || prop.SetMethod.IsAssembly)
            //{
            //    return "internal";
            //}

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
