using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels
{
    public class ExpanderViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TemplateFolder { get; set; }
        public int Order { get; set; }
        public List<AppViewModel> Apps { get; set; }
        public List<ComponentViewModel> Components { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}