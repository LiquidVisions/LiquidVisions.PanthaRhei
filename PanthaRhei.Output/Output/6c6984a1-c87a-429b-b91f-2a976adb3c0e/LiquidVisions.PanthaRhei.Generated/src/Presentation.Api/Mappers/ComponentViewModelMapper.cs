using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ComponentViewModelMapper : Profile
    {
        public ComponentViewModelMapper()
        {
            CreateMap<Component, ComponentViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.Description, x => x.MapFrom(source => source.Description))
                .ForMember(target => target.Packages, x => x.MapFrom(source => source.Packages))
                .ForMember(target => target.Expander, x => x.MapFrom(source => source.Expander));
        }
    }
}
