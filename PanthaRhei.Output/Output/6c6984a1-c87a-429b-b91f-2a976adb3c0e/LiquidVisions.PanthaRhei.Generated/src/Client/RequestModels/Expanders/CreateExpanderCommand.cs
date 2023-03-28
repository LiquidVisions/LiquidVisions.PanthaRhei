﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Expanders
{
    public class CreateExpanderCommand : RequestModel
    {
        public string Name { get; set; }
        public string TemplateFolder { get; set; }
        public int Order { get; set; }
        public List<App> Apps { get; set; }
        public List<Component> Components { get; set; }
    }
}
