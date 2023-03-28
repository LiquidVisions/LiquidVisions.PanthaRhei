using LiquidVisions.PanthaRhei.Generated.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Entities
{
    public interface IDeleteEntityPresenter : IIResultPresenter {}

    public class DeleteEntityPresenter : IDeleteEntityPresenter
    {


        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null) => Response.IsValid ? 
            Results.Ok() : 
            Response.ToWebApiResult(request);
    }
}