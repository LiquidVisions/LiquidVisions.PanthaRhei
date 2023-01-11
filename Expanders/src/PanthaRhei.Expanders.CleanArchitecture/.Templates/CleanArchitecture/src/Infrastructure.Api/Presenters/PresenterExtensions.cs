using System;
using System.Linq;
using NS.Application;
using NS.Client.ViewModels;
using Microsoft.AspNetCore.Http;

namespace NS.Infrastructure.Api.Presenters
{
    internal static class PresenterExtensions
    {
        internal static IResult ToWebApiResult(this Response response, HttpRequest request)
        {
            if (!response.IsValid)
            {
                var errors = new ErrorViewModel
                {
                    Path = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}",
                    Details = response.Errors.Select(x => new ErrorDetails { StatusCode = x.ErrorCode.Code, Message = x.ErrorCode.Message, Description = x.Message }).ToList(),
                };

                return Results.Json(
                    options: new System.Text.Json.JsonSerializerOptions { IncludeFields = true, },
                    statusCode: response.Errors.Count > 1 ? 500 : response.Errors.Single().ErrorCode.Code,
                    data: errors,
                    contentType: "application/json");
            }

            throw new InvalidOperationException();
        }
    }
}
