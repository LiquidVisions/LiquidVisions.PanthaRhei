﻿using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Expanders
{
    public interface IUpdateExpanderPresenter : IIResultPresenter {}

    public class UpdateExpanderPresenter : IUpdateExpanderPresenter
    {
        private readonly IMapper mapper;

        public UpdateExpanderPresenter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Ok(mapper.Map<ExpanderViewModel>(Response.GetParameter<Expander>())) :
                Response.ToWebApiResult(request);
        }
    }
}