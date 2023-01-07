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
        private Dictionary<Type, string[]> keys = new Dictionary<Type, string[]>();
        private Dictionary<Type, string[]> indexes = new Dictionary<Type, string[]>();

        public ModelConfiguration(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public string[] GetIndexes(Type entityType)
        {
            return modelBuilder.Entity(entityType).Metadata
                .GetIndexes().Select(x => x.Name).ToArray();
        }

        public string[] GetKeys(Type entityType)
        {
            return modelBuilder.Entity(entityType).Metadata
                .GetKeys().Select(x => x.GetName()).ToArray();
        }

        private void Load(ModelBuilder modelBuilder)
        {
            //foreach (var entity in modelBuilder. .Entity(typeof(App)).Metadata.)
            //{
            //    keys.Add(entity.GetType(), entity.Met)
            //}
        }

        //public string[] GetIndexes(Type entityType)
        //    => indexes[entityType];

        //public string[] GetKeys(Type entityType)
        //    => keys[entityType];
    }
}
