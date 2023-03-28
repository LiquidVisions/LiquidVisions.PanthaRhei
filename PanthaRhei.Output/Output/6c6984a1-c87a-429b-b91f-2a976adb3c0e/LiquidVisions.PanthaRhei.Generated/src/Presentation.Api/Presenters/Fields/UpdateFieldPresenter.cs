using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Fields
{
    public interface IUpdateFieldPresenter : IIResultPresenter {}

    public class UpdateFieldPresenter : IUpdateFieldPresenter
    {
        private readonly IMapper<Field, FieldViewModel> mapper;

        public UpdateFieldPresenter(IMapper<Field, FieldViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Ok(mapper.Map(Response.GetParameter<Field>())) :
                Response.ToWebApiResult(request);
        }
    }
}