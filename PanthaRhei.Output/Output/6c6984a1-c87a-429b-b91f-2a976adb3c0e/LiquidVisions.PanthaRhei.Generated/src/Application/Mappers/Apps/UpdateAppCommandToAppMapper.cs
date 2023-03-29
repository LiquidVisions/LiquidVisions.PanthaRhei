﻿using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Apps
{
    internal class UpdateAppCommandToAppMapper : IMapper<UpdateAppCommand, App>
    {
        public void Map(UpdateAppCommand source, App target)
        {
            target.Name = source.Name;
            target.FullName = source.FullName;
            target.Expanders = source.Expanders;
            target.Entities = source.Entities;
            target.ConnectionStrings = source.ConnectionStrings;
        }

        public App Map(UpdateAppCommand source)
        {
            App target = new();

            Map(source, target);

            return target;
        }
    }
}
