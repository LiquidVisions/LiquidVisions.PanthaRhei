using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Models;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework
{
    internal class ModelConfiguration : IModelConfiguration
    {
        private readonly DbContext context;

        public ModelConfiguration(DbContext context)
        {
            this.context = context;
        }

        public string[] GetIndexes(Type entityType)
        {
            return context.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType)
                .GetIndexes().SelectMany(x => x.Properties.Select(p => p.Name))
                .ToArray();
        }

        public string[] GetKeys(Type entityType)
        {
            return context.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType)
                .GetKeys().SelectMany(x => x.Properties.Select(p => p.Name))
                .ToArray();
        }

        public int? GetSize(Type entityType, string propName)
        {
            var entity = context.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType);

            IProperty prop = entity.FindProperty(propName);
            if (prop != null)
            {
                return prop.GetMaxLength();
            }

            return null;
        }

        public bool GetIsRequired(Type entityType, string propName)
        {
            var entity = context.Model.GetEntityTypes()
                    .Single(x => x.ClrType == entityType);

            var prop = entity.FindProperty(propName);
            if (prop != null)
            {
                return !prop.IsNullable;
            }

            var navigationProperty = entity.FindNavigation(propName);
            if (navigationProperty != null)
            {
                return !navigationProperty.ForeignKey.IsRequired;
            }

            return false;
        }

        public List<RelationshipDto> GetRelationshipInfo(Entity entity)
        {
            var mutableEntity = context.Model.GetEntityTypes()
                .Single(x => x.ClrType.Name == entity.Name);

            List<RelationshipDto> result = new();

            var navigations = mutableEntity.GetNavigations().Where(x => x.ForeignKey.DeclaringEntityType.ClrType.Name == entity.Name).ToList();

            foreach (var navigation in navigations)
            {
                result.Add(new RelationshipDto
                {
                    Entity = mutableEntity.ClrType.Name,
                    Key = navigation.Name,
                    Cardinality = GetCardinality(navigation.ClrType),
                    WithForeignEntity = navigation.ForeignKey.DependentToPrincipal.ClrType.Name,
                    WithForeignEntityKey = navigation.ForeignKey.GetNavigation(false).PropertyInfo.Name,
                    WithyCardinality = GetCardinality(navigation.ForeignKey.GetNavigation(false).ClrType),
                    Required = navigation.ForeignKey.IsRequired,
                });
            }

            return result;
        }

        private static string GetCardinality(Type type)
        {
            return type.IsGenericType && type.GetInterfaces()
                .Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                ? "WithMany"
                : "WithOne";
        }
    }
}
