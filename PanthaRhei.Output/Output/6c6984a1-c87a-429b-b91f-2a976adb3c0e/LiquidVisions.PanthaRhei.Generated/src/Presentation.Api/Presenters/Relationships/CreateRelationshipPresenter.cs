using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Relationships
{
    public interface ICreateRelationshipPresenter : IIResultPresenter {}

    public class CreateRelationshipPresenter : ICreateRelationshipPresenter
    {
        private readonly IMapper<Relationship, RelationshipViewModel> mapper;

        public CreateRelationshipPresenter(IMapper<Relationship, RelationshipViewModel> mapper)
        {
            this.mapper = mapper;
        }

        public Response Response { get; set; }

        public IResult GetResult(HttpRequest request = null)
        {

            return Response.IsValid ?
                Results.Created($"//{mapper.Map(Response.GetParameter<Relationship>()).Id}", mapper.Map(Response.GetParameter<Relationship>())) :
                Response.ToWebApiResult(request);
        }
    }
}