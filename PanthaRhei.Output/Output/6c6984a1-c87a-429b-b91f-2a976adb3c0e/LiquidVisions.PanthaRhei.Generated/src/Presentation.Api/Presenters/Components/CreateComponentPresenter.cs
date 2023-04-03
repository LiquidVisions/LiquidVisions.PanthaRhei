using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
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
    public interface ICreateComponentPresenter : IIResultPresenter {}

    public class CreateComponentPresenter : ICreateComponentPresenter
    {
        private readonly IMapper<Component, ComponentViewModel> mapper;

        public CreateComponentPresenter(IMapper<Component, ComponentViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Created($"//{mapper.Map(Response.GetParameter<Component>()).Id}", mapper.Map(Response.GetParameter<Component>())) :
                Response.ToWebApiResult(request);
        }
    }
}