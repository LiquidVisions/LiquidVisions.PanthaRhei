using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ConnectionStringViewModelMapper : Profile
    {
        public ConnectionStringViewModelMapper()
        {
            CreateMap<ConnectionString, ConnectionStringViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.Definition, x => x.MapFrom(source => source.Definition))
                .ForMember(target => target.App, x => x.MapFrom(source => source.App));
        }
    }
}
