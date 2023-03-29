using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Client.ViewModels
{
    public class PackageViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public ComponentViewModel Component { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}