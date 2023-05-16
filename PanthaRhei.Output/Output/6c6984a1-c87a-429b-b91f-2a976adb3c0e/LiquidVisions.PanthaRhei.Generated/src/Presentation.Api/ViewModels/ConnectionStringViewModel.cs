using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels
{
    public class ConnectionStringViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public AppViewModel App { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}