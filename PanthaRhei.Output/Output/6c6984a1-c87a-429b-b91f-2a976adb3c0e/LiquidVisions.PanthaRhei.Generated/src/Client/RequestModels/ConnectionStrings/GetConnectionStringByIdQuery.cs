using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings
{
    public class GetConnectionStringByIdQuery : RequestModel
    {
            public Guid Id { get; set; }
    }
}
