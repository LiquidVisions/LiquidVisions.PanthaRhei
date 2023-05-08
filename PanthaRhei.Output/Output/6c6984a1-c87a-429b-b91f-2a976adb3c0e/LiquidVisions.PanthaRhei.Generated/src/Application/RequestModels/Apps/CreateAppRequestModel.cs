﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps
{
    public class CreateAppRequestModel : RequestModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<Expander> Expanders { get; set; }
        public List<Entity> Entities { get; set; }
        public List<ConnectionString> ConnectionStrings { get; set; }
    }
}