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
    public interface IGetByIdComponentPresenter : IIResultPresenter {}

    public class GetByIdComponentPresenter : IGetByIdComponentPresenter
    {
        private readonly IMapper<Component, ComponentViewModel> mapper;

        public GetByIdComponentPresenter(IMapper<Component, ComponentViewModel> mapper)
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
                Results.Json(mapper.Map(Response.GetParameter<Component>()), options, "application/json", 200) :
                Response.ToWebApiResult(request);
        }
    }
}