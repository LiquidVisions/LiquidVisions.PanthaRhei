﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings
{
    public class DeleteConnectionStringCommand : RequestModel
    {
            public Guid Id { get; set; }
    }
}