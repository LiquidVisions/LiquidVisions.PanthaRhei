using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields
{
    public class CreateFieldRequestModel : RequestModel
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public bool IsCollection { get; set; }
        public string Modifier { get; set; }
        public string GetModifier { get; set; }
        public string SetModifier { get; set; }
        public string Behaviour { get; set; }
        public int Order { get; set; }
        public int Size { get; set; }
        public bool Required { get; set; }
        public Entity Reference { get; set; }
        public Entity Entity { get; set; }
        public bool IsKey { get; set; }
        public bool IsIndex { get; set; }
        public List<Relationship> RelationshipKeys { get; set; }
        public List<Relationship> IsForeignEntityKeyOf { get; set; }
    }
}
