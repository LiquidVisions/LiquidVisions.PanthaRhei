﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships
{
    public class GetRelationshipByIdRequestModel : RequestModel
    {
            public Guid Id { get; set; }
    }
}