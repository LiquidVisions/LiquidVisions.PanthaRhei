using LiquidVisions.PanthaRhei.Generated.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Components
{
    public interface IDeleteComponentPresenter : IIResultPresenter {}

    public class DeleteComponentPresenter : IDeleteComponentPresenter
    {


        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null) => Response.IsValid ? 
            Results.Ok() : 
            Response.ToWebApiResult(request);
    }
}