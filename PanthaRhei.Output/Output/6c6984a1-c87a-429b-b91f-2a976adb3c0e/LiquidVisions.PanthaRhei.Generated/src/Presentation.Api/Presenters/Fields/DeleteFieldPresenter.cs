using LiquidVisions.PanthaRhei.Generated.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Fields
{
    public interface IDeleteFieldPresenter : IIResultPresenter {}

    public class DeleteFieldPresenter : IDeleteFieldPresenter
    {


        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null) => Response.IsValid ? 
            Results.Ok() : 
            Response.ToWebApiResult(request);
    }
}