using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships
{
    public class DeleteRelationshipCommand : RequestModel
    {
            public Guid Id { get; set; }
    }
}
