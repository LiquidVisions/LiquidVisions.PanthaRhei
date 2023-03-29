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
    public interface IGetExpandersPresenter : IIResultPresenter {}

    public class GetExpandersPresenter : IGetExpandersPresenter
    {
        private readonly IMapper<Expander, ExpanderViewModel> mapper;

        public GetExpandersPresenter(IMapper<Expander, ExpanderViewModel> mapper)
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
                Results.Json(Response.GetParameter<List<Expander>>().Select(x => mapper.Map(x)), options, "application/json", 200) :
                Response.ToWebApiResult(request);
        }
    }
}