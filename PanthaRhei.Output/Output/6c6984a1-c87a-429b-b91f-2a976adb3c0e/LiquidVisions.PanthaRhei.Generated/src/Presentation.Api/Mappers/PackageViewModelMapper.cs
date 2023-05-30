using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class PackageViewModelMapper : Profile
    {
        public PackageViewModelMapper()
        {
            CreateMap<Package, PackageViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.Version, x => x.MapFrom(source => source.Version))
                .ForMember(target => target.Component, x => x.MapFrom(source => source.Component));
        }
    }
}
