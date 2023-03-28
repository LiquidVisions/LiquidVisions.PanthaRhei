using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Client.ViewModels
{
    public class EntityViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Callsite { get; set; }
        public string Type { get; set; }
        public string Modifier { get; set; }
        public string Behaviour { get; set; }
        public AppViewModel App { get; set; }
        public List<FieldViewModel> Fields { get; set; }
        public List<FieldViewModel> ReferencedIn { get; set; }
        public List<RelationshipViewModel> Relations { get; set; }
        public List<RelationshipViewModel> IsForeignEntityOf { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}