using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Client.ViewModels
{
    public class ComponentViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PackageViewModel> Packages { get; set; }
        public ExpanderViewModel Expander { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}