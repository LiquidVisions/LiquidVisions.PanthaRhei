using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities
{
    public class DeleteEntityCommand : RequestModel
    {
            public Guid Id { get; set; }
    }
}
