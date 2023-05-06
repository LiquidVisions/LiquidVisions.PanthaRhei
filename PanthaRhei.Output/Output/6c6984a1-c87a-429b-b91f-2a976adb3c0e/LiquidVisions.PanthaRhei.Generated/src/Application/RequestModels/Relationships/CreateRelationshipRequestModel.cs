using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships
{
    public class CreateRelationshipRequestModel : RequestModel
    {
        public Field Key { get; set; }
        public Entity Entity { get; set; }
        public string Cardinality { get; set; }
        public Field WithForeignEntityKey { get; set; }
        public Entity WithForeignEntity { get; set; }
        public string WithCardinality { get; set; }
        public bool Required { get; set; }
    }
}
