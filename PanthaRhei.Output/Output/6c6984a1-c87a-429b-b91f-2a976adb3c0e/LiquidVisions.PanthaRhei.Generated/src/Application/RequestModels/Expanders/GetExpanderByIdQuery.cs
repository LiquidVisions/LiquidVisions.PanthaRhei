﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders
{
    public class GetExpanderByIdQuery : RequestModel
    {
            public Guid Id { get; set; }
    }
}