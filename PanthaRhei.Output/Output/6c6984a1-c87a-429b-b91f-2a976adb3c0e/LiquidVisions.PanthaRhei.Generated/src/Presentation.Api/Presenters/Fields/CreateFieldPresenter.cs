using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Fields
{
    public interface ICreateFieldPresenter : IIResultPresenter {}

    public class CreateFieldPresenter : ICreateFieldPresenter
    {
        private readonly IMapper mapper;

        public CreateFieldPresenter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Created($"//{mapper.Map<FieldViewModel>(Response.GetParameter<Field>()).Id}", mapper.Map<FieldViewModel>(Response.GetParameter<Field>())) :
                Response.ToWebApiResult(request);
        }
    }
}