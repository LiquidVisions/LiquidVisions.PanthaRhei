﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components
{
    public class GetComponentByIdQuery : RequestModel
    {
            public Guid Id { get; set; }
    }
}
