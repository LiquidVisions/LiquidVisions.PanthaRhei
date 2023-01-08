using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework
{
    public class ModelConfiguration : IModelConfiguration
    {
        private readonly ModelBuilder modelBuilder;

        public ModelConfiguration(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public string[] GetIndexes(Type entityType)
        {
            return modelBuilder.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType)
                .GetIndexes().SelectMany(x => x.Properties.Select(p => p.Name))
                .ToArray();
        }

        public string[] GetKeys(Type entityType)
        {
            return modelBuilder.Model.GetEntityTypes()
                .Single(x => x.ClrType == entityType)
                .GetKeys().SelectMany(x => x.Properties.Select(p => p.Name))
                .ToArray();
        }

        public List<Dictionary<string, string>> GetRelationshipInfo(Entity entity)
        {
            var mutableEntity = modelBuilder.Model.GetEntityTypes()
                .Single(x => x.ClrType.Name == entity.Name);

            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            foreach (var fk in mutableEntity.GetNavigations().Where(x => x.ForeignKey.DeclaringEntityType.ClrType.Name == mutableEntity.ClrType.Name))
            {
                Dictionary<string, string> pairs = new()
                {
                    { nameof(Relationship.Entity), mutableEntity.ClrType.Name },
                    { nameof(Relationship.Key), fk.Name },
                    { nameof(Relationship.Cardinality), GetCardinality(fk.ClrType) },

                    { nameof(Relationship.WithForeignEntity), fk.ForeignKey.DependentToPrincipal.ClrType.Name },
                    { nameof(Relationship.WithForeignEntityKey), fk.ForeignKey.GetNavigation(false).PropertyInfo.Name },
                    { nameof(Relationship.WithyCardinality), GetCardinality(fk.ForeignKey.GetNavigation(false).ClrType) },
                };

                result.Add(pairs);
            }

            return result;
        }

        public string GetCardinality(Type type)
        {
            return type.IsGenericType && type.GetInterfaces()
                .Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                ? "WithMany"
                : "WithOne";
        }
    }
}
