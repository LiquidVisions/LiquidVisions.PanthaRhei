using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Client.ViewModels
{
    public class RelationshipViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public FieldViewModel Key { get; set; }
        public EntityViewModel Entity { get; set; }
        public string Cardinality { get; set; }
        public FieldViewModel WithForeignEntityKey { get; set; }
        public EntityViewModel WithForeignEntity { get; set; }
        public string WithCardinality { get; set; }
        public bool Required { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}