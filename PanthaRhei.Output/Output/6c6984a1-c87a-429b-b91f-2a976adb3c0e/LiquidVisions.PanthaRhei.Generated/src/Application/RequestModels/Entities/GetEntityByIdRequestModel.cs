using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities
{
    public class GetEntityByIdRequestModel : RequestModel
    {
            public Guid Id { get; set; }
    }
}
