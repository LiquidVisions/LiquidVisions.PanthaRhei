using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Expanders
{
    public interface ICreateExpanderPresenter : IIResultPresenter {}

    public class CreateExpanderPresenter : ICreateExpanderPresenter
    {
        private readonly IMapper<Expander, ExpanderViewModel> mapper;

        public CreateExpanderPresenter(IMapper<Expander, ExpanderViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Created($"//{mapper.Map(Response.GetParameter<Expander>()).Id}", mapper.Map(Response.GetParameter<Expander>())) :
                Response.ToWebApiResult(request);
        }
    }
}