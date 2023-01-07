using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using Microsoft.EntityFrameworkCore;

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
    }
}
