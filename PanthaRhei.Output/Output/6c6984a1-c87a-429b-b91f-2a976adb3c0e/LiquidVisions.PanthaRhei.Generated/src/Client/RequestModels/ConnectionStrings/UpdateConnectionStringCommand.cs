using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings
{
    public class UpdateConnectionStringCommand : RequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public App App { get; set; }
    }
}
