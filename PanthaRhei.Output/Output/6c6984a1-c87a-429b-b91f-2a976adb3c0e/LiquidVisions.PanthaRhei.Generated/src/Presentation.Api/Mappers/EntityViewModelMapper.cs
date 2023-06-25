using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class EntityViewModelMapper : Profile
    {
        public EntityViewModelMapper()
        {
            CreateMap<Entity, EntityViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.Callsite, x => x.MapFrom(source => source.Callsite))
                .ForMember(target => target.Type, x => x.MapFrom(source => source.Type))
                .ForMember(target => target.Modifier, x => x.MapFrom(source => source.Modifier))
                .ForMember(target => target.Behaviour, x => x.MapFrom(source => source.Behaviour))
                .ForMember(target => target.App, x => x.MapFrom(source => source.App))
                .ForMember(target => target.Fields, x => x.MapFrom(source => source.Fields))
                .ForMember(target => target.ReferencedIn, x => x.MapFrom(source => source.ReferencedIn))
                .ForMember(target => target.Relations, x => x.MapFrom(source => source.Relations))
                .ForMember(target => target.IsForeignEntityOf, x => x.MapFrom(source => source.IsForeignEntityOf));
        }
    }
}
