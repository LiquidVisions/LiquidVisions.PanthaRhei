using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders
{
    public class UpdateExpanderCommand : RequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TemplateFolder { get; set; }
        public int Order { get; set; }
        public List<App> Apps { get; set; }
        public List<Component> Components { get; set; }
    }
}
