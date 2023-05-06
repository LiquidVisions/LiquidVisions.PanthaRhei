using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities
{
    public class CreateEntityRequestModel : RequestModel
    {
        public string Name { get; set; }
        public string Callsite { get; set; }
        public string Type { get; set; }
        public string Modifier { get; set; }
        public string Behaviour { get; set; }
        public App App { get; set; }
        public List<Field> Fields { get; set; }
        public List<Field> ReferencedIn { get; set; }
        public List<Relationship> Relations { get; set; }
        public List<Relationship> IsForeignEntityOf { get; set; }
    }
}
