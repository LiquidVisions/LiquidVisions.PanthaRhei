﻿using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ConnectionStringViewModelMapper : IMapper<ConnectionString, ConnectionStringViewModel>
    {
        public void Map(ConnectionString source, ConnectionStringViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Definition = source.Definition;
            target.App = source.App != null ? new AppViewModelMapper().Map(source.App) : null;
        }

        public ConnectionStringViewModel Map(ConnectionString source)
        {
			ConnectionStringViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
