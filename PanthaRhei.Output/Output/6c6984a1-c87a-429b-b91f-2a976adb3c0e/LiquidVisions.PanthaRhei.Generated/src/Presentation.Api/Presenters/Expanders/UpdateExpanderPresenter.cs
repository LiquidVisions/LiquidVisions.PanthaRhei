using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
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
    public interface IUpdateExpanderPresenter : IIResultPresenter {}

    public class UpdateExpanderPresenter : IUpdateExpanderPresenter
    {
        private readonly IMapper<Expander, ExpanderViewModel> mapper;

        public UpdateExpanderPresenter(IMapper<Expander, ExpanderViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Ok(mapper.Map(Response.GetParameter<Expander>())) :
                Response.ToWebApiResult(request);
        }
    }
}