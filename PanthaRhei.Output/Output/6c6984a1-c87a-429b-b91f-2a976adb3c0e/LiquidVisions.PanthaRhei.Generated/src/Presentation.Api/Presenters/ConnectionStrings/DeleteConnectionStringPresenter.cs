using LiquidVisions.PanthaRhei.Generated.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.ConnectionStrings
{
    public interface IDeleteConnectionStringPresenter : IIResultPresenter {}

    public class DeleteConnectionStringPresenter : IDeleteConnectionStringPresenter
    {


        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null) => Response.IsValid ? 
            Results.Ok() : 
            Response.ToWebApiResult(request);
    }
}