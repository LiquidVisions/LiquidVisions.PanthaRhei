using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings
{
    public class CreateConnectionStringRequestModel : RequestModel
    {
        public string Name { get; set; }
        public string Definition { get; set; }
        public App App { get; set; }
    }
}
