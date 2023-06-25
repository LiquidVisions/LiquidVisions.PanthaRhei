using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class AppViewModelMapper : Profile
    {
        public AppViewModelMapper()
        {
            CreateMap<App, AppViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.FullName, x => x.MapFrom(source => source.FullName))
                .ForMember(target => target.Expanders, x => x.MapFrom(source => source.Expanders))
                .ForMember(target => target.Entities, x => x.MapFrom(source => source.Entities))
                .ForMember(target => target.ConnectionStrings, x => x.MapFrom(source => source.ConnectionStrings));
        }
    }
}
