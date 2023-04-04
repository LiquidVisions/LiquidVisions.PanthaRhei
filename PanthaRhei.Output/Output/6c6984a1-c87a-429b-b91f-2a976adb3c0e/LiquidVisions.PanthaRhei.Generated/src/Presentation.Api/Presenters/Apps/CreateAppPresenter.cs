﻿using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Apps
{
    public interface ICreateAppPresenter : IIResultPresenter {}

    public class CreateAppPresenter : ICreateAppPresenter
    {
        private readonly IMapper<App, AppViewModel> mapper;

        public CreateAppPresenter(IMapper<App, AppViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Created($"//{mapper.Map(Response.GetParameter<App>()).Id}", mapper.Map(Response.GetParameter<App>())) :
                Response.ToWebApiResult(request);
        }
    }
}