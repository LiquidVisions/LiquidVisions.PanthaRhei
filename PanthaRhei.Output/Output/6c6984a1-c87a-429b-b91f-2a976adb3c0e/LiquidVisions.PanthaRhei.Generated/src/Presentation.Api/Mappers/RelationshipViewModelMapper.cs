using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class RelationshipViewModelMapper : Profile
    {
        public RelationshipViewModelMapper()
        {
            CreateMap<Relationship, RelationshipViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Key, x => x.MapFrom(source => source.Key))
                .ForMember(target => target.Entity, x => x.MapFrom(source => source.Entity))
                .ForMember(target => target.Cardinality, x => x.MapFrom(source => source.Cardinality))
                .ForMember(target => target.WithForeignEntityKey, x => x.MapFrom(source => source.WithForeignEntityKey))
                .ForMember(target => target.WithForeignEntity, x => x.MapFrom(source => source.WithForeignEntity))
                .ForMember(target => target.WithCardinality, x => x.MapFrom(source => source.WithCardinality))
                .ForMember(target => target.Required, x => x.MapFrom(source => source.Required));
        }
    }
}
