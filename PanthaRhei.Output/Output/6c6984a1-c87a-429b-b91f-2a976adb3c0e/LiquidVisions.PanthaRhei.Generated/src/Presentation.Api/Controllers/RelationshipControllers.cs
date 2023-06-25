using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Relationships;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers
{
    public static class RelationshipController
    {
        private static readonly string endpointTemplate = "/relationships";

        public static IServiceCollection AddRelationshipElements(this IServiceCollection services)
        {
            //services.AddTransient<IMapper<Relationship, RelationshipViewModel>, RelationshipViewModelMapper>();
            services.AddTransient<ICreateRelationshipPresenter, CreateRelationshipPresenter>();
            services.AddTransient<IGetByIdRelationshipPresenter, GetByIdRelationshipPresenter>();
            services.AddTransient<IGetRelationshipsPresenter, GetRelationshipsPresenter>();
            services.AddTransient<IUpdateRelationshipPresenter, UpdateRelationshipPresenter>();
            services.AddTransient<IDeleteRelationshipPresenter, DeleteRelationshipPresenter>();

            return services;
        }

        public static void UseRelationshipEndpoints(this WebApplication app)
        {
            app.MapCreateRelationship()
                .MapGetRelationship()
                .MapGetRelationships()
                .MapUpdateRelationship()
                .MapDeleteRelationship();
        }

        private static WebApplication MapCreateRelationship(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateRelationshipRequestModel model, IBoundary<CreateRelationshipRequestModel> boundary, ICreateRelationshipPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(RelationshipViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Relationships");

            return app;
        }

        private static WebApplication MapGetRelationship(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetRelationshipByIdRequestModel> boundary, IGetByIdRelationshipPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetRelationshipByIdRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(RelationshipViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Relationships");

            return app;
        }

        private static WebApplication MapGetRelationships(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetRelationshipsRequestModel> boudary, IGetRelationshipsPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetRelationshipsRequestModel(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(RelationshipViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Relationships");

            return app;
        }

        private static WebApplication MapUpdateRelationship(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateRelationshipRequestModel model, IBoundary<UpdateRelationshipRequestModel> boundary, IUpdateRelationshipPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(RelationshipViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Relationships");

            return app;
        }

        private static WebApplication MapDeleteRelationship(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteRelationshipRequestModel> boundary, IDeleteRelationshipPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteRelationshipRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(RelationshipViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Relationships");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
