using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework
{
    public class ModelConfiguration : IModelConfiguration
    {
        private Dictionary<Type, string[]> keys = new Dictionary<Type, string[]>();

        public ModelConfiguration(ModelBuilder modelBuilder)
        {
            
        }

        private void Setup(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Entity(typeof(App)).Metadata.)
            {
                keys.Add(entity.GetType(), entity.Met)
            }
        }

        public string[] GetIndexes(Type entityType)
        {
            return modelBuilder.Entity(entityType)
                .Metadata
                .GetIndexes()
                .Select(x => x.Name)
                .ToArray();
        }

        public string[] GetKeys(Type entityType)
        {
            return modelBuilder.Entity(entityType)
                .Metadata
                .GetKeys()
                .Select(x => x.GetName())
                .ToArray();
        }
    }
}
