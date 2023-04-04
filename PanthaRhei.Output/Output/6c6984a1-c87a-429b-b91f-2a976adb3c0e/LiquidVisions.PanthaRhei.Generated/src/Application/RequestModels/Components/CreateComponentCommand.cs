﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components
{
    public class CreateComponentCommand : RequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Package> Packages { get; set; }
        public Expander Expander { get; set; }
    }
}