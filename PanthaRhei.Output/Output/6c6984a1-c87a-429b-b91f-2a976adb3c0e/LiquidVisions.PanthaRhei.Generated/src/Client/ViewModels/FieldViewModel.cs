using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Client.ViewModels
{
    public class FieldViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public bool IsCollection { get; set; }
        public string Modifier { get; set; }
        public string GetModifier { get; set; }
        public string SetModifier { get; set; }
        public string Behaviour { get; set; }
        public int Order { get; set; }
        public int? Size { get; set; }
        public bool Required { get; set; }
        public EntityViewModel Reference { get; set; }
        public EntityViewModel Entity { get; set; }
        public bool IsKey { get; set; }
        public bool IsIndex { get; set; }
        public List<RelationshipViewModel> RelationshipKeys { get; set; }
        public List<RelationshipViewModel> IsForeignEntityKeyOf { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}