using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Models;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework
{
    internal sealed class ModelConfiguration(DbContext context) : IModelConfiguration
    {
        public IEnumerable<string> GetIndexes(Type entityType)
        {
            return context.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType)
                .GetIndexes().SelectMany(x => x.Properties.Select(p => p.Name));
        }

        public IEnumerable<string> GetKeys(Type entityType)
        {
            return context.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType)
                .GetKeys().SelectMany(x => x.Properties.Select(p => p.Name));
        }

        public int? GetSize(Type entityType, string propertyName)
        {
            IEntityType entity = context.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType);

            IProperty prop = entity.FindProperty(propertyName);
            if (prop != null)
            {
                return prop.GetMaxLength();
            }

            return null;
        }

        public bool GetIsRequired(Type entityType, string propertyName)
        {
            IEntityType entity = context.Model.GetEntityTypes()
                    .Single(x => x.ClrType == entityType);

            IProperty prop = entity.FindProperty(propertyName);
            if (prop != null)
            {
                return !prop.IsNullable;
            }

            INavigation navigationProperty = entity.FindNavigation(propertyName);
            if (navigationProperty != null)
            {
                return !navigationProperty.ForeignKey.IsRequired;
            }

            return false;
        }

        public ICollection<RelationshipDto> GetRelationshipInfo(Entity entity)
        {
            IEntityType mutableEntity = context.Model.GetEntityTypes()
                .Single(x => x.ClrType.Name == entity.Name);

            List<RelationshipDto> result = [];

            var navigations = mutableEntity.GetNavigations().Where(x => x.ForeignKey.DeclaringEntityType.ClrType.Name == entity.Name).ToList();

            foreach (INavigation navigation in navigations)
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
                .AsEnumerable()
                .Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                ? "WithMany"
                : "WithOne";
        }
    }
}
