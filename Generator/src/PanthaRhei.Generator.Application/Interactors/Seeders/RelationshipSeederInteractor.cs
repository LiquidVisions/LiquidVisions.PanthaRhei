using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{

    internal class RelationshipSeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericGateway<Relationship> gateway;
        private readonly Parameters parameters;
        private readonly IDependencyFactoryInteractor dependencyFactory;

        public RelationshipSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGenericGateway<Relationship>>();
            parameters = dependencyFactory.Get<Parameters>();
            this.dependencyFactory = dependencyFactory;
        }

        public int SeedOrder => 7;

        public int ResetOrder => 0;

        public void Seed(App app)
        {
            // CODESMELL, I cannot resolve the ImodelConfiguration in constructor.
            var modelConfiguration = dependencyFactory.Get<IModelConfiguration>();

            foreach (Entity entity in app.Entities)
            {
                List<Dictionary<string, string>> infos = modelConfiguration.GetRelationshipInfo(entity);

                foreach (var info in infos)
                {
                    Relationship relationship = new() { Id = Guid.NewGuid() };

                    // Entity
                    relationship.Entity = entity;
                    entity.Relations.Add(relationship);

                    // Key
                    relationship.Key = entity.Fields.Single(x => x.Name == info[nameof(Relationship.Key)]);
                    relationship.Key.RelationshipKeys.Add(relationship);

                    // Cardinality
                    relationship.Cardinality = info[nameof(Relationship.Cardinality)];

                    // WithForeignEntity
                    relationship.WithForeignEntity = entity.App.Entities.Single(x => x.Name == info[nameof(Relationship.WithForeignEntity)]);
                    relationship.WithForeignEntity.IsForeignEntityOf.Add(relationship);

                    // WithForeignEntityKey
                    relationship.WithForeignEntityKey = relationship.WithForeignEntity.Fields.Single(x => x.Name == info[nameof(Relationship.WithForeignEntityKey)]);
                    relationship.WithForeignEntityKey.IsForeignEntityKeyOf.Add(relationship);

                    // WithyCardinality
                    relationship.WithyCardinality = info[nameof(Relationship.WithyCardinality)];

                    gateway.Create(relationship);
                }
            }
        }

        public void Reset() => gateway.DeleteAll();
    }
}
