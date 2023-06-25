using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ExpanderViewModelMapper : Profile
    {
        public ExpanderViewModelMapper()
        {
            CreateMap<Expander, ExpanderViewModel>()
                .ForMember(target => target.Id, x => x.MapFrom(source => source.Id))
                .ForMember(target => target.Name, x => x.MapFrom(source => source.Name))
                .ForMember(target => target.TemplateFolder, x => x.MapFrom(source => source.TemplateFolder))
                .ForMember(target => target.Order, x => x.MapFrom(source => source.Order))
                .ForMember(target => target.Apps, x => x.MapFrom(source => source.Apps))
                .ForMember(target => target.Components, x => x.MapFrom(source => source.Components));
        }
    }
}
