using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields
{
    public class DeleteFieldCommand : RequestModel
    {
            public Guid Id { get; set; }
    }
}
