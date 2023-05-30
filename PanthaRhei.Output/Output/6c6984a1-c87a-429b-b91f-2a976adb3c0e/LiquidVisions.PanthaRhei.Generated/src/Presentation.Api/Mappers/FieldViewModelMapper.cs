using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class FieldViewModelMapper : Profile
    {
        public FieldViewModelMapper()
        {
            CreateMap<Field, FieldViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.ReturnType, x => x.MapFrom(source => source.ReturnType))
                .ForMember(target => target.IsCollection, x => x.MapFrom(source => source.IsCollection))
                .ForMember(target => target.Modifier, x => x.MapFrom(source => source.Modifier))
                .ForMember(target => target.GetModifier, x => x.MapFrom(source => source.GetModifier))
                .ForMember(target => target.SetModifier, x => x.MapFrom(source => source.SetModifier))
                .ForMember(target => target.Behaviour, x => x.MapFrom(source => source.Behaviour))
                .ForMember(target => target.Order, x => x.MapFrom(source => source.Order))
                .ForMember(target => target.Size, x => x.MapFrom(source => source.Size))
                .ForMember(target => target.Required, x => x.MapFrom(source => source.Required))
                .ForMember(target => target.Reference, x => x.MapFrom(source => source.Reference))
                .ForMember(target => target.Entity, x => x.MapFrom(source => source.Entity))
                .ForMember(target => target.IsKey, x => x.MapFrom(source => source.IsKey))
                .ForMember(target => target.IsIndex, x => x.MapFrom(source => source.IsIndex))
                .ForMember(target => target.RelationshipKeys, x => x.MapFrom(source => source.RelationshipKeys))
                .ForMember(target => target.IsForeignEntityKeyOf, x => x.MapFrom(source => source.IsForeignEntityKeyOf));
        }
    }
}
