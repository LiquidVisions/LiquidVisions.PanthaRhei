using LiquidVisions.PanthaRhei.Generated.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Apps
{
    public interface IDeleteAppPresenter : IIResultPresenter {}

    public class DeleteAppPresenter : IDeleteAppPresenter
    {


        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null) => Response.IsValid ? 
            Results.Ok() : 
            Response.ToWebApiResult(request);
    }
}