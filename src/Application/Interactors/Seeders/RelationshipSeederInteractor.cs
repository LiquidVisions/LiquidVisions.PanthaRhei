using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Models;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Seeders
{
    internal class RelationshipSeederInteractor : IEntitySeederInteractor<App>
    {
        private readonly ICreateGateway<Relationship> createGateway;
        private readonly IDeleteGateway<Relationship> deleteGateway;
        private readonly IModelConfiguration modelConfiguration;

        public RelationshipSeederInteractor(IDependencyFactory dependencyFactory)
        {
            createGateway = dependencyFactory.Get<ICreateGateway<Relationship>>();
            deleteGateway = dependencyFactory.Get<IDeleteGateway<Relationship>>();
            modelConfiguration = dependencyFactory.Get<IModelConfiguration>();
        }

        public int SeedOrder => 7;

        public int ResetOrder => 0;

        public void Seed(App app)
        {
            foreach (Entity entity in app.Entities)
            {
                List<RelationshipDto> infos = modelConfiguration.GetRelationshipInfo(entity);

                foreach (var info in infos)
                {
                    Relationship relationship = new()
                    {
                        Id = Guid.NewGuid(),
                        Entity = entity,
                    };

                    entity.Relations.Add(relationship);

                    // Key
                    relationship.Key = entity.Fields.Single(x => x.Name == info.Key);
                    relationship.Key.RelationshipKeys.Add(relationship);

                    // Cardinality
                    relationship.Cardinality = info.Cardinality;

                    // WithForeignEntity
                    relationship.WithForeignEntity = entity.App.Entities.Single(x => x.Name == info.WithForeignEntity);
                    relationship.WithForeignEntity.IsForeignEntityOf.Add(relationship);

                    // WithForeignEntityKey
                    relationship.WithForeignEntityKey = relationship.WithForeignEntity.Fields.Single(x => x.Name == info.WithForeignEntityKey);
                    relationship.WithForeignEntityKey.IsForeignEntityKeyOf.Add(relationship);

                    // WithyCardinality
                    relationship.WithCardinality = info.WithyCardinality;

                    relationship.Required = info.Required;

                    createGateway.Create(relationship);
                }
            }
        }

        public void Reset() => deleteGateway.DeleteAll();
    }
}
