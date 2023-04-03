using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels
{
    public class AppViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<ExpanderViewModel> Expanders { get; set; }
        public List<EntityViewModel> Entities { get; set; }
        public List<ConnectionStringViewModel> ConnectionStrings { get; set; }

        #region ns-custom-properties
        #endregion ns-custom-properties
    }
}