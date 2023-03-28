using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.ConnectionStrings
{
    public interface ICreateConnectionStringPresenter : IIResultPresenter {}

    public class CreateConnectionStringPresenter : ICreateConnectionStringPresenter
    {
        private readonly IMapper<ConnectionString, ConnectionStringViewModel> mapper;

        public CreateConnectionStringPresenter(IMapper<ConnectionString, ConnectionStringViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Created($"//{mapper.Map(Response.GetParameter<ConnectionString>()).Id}", mapper.Map(Response.GetParameter<ConnectionString>())) :
                Response.ToWebApiResult(request);
        }
    }
}