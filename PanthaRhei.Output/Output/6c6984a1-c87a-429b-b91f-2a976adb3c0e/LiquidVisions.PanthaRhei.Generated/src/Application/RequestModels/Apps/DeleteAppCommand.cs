﻿using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps
{
    public class DeleteAppCommand : RequestModel
    {
            public Guid Id { get; set; }
    }
}