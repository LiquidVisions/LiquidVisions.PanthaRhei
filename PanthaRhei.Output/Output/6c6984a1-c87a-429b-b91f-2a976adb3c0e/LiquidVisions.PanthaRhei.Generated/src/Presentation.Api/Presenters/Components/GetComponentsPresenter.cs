using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Components
{
    public interface IGetComponentsPresenter : IIResultPresenter {}

    public class GetComponentsPresenter : IGetComponentsPresenter
    {
        private readonly IMapper<Component, ComponentViewModel> mapper;

        public GetComponentsPresenter(IMapper<Component, ComponentViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true,
                PropertyNameCaseInsensitive= false,
            };

            return Response.IsValid ?
                Results.Json(Response.GetParameter<List<Component>>().Select(x => mapper.Map(x)), options, "application/json", 200) :
                Response.ToWebApiResult(request);
        }
    }
}