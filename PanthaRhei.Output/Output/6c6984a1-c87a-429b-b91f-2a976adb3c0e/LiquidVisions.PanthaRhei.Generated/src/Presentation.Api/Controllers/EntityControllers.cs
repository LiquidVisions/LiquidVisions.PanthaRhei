using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Entities;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers
{
    public static class EntityController
    {
        private static readonly string endpointTemplate = "/entities";

        public static IServiceCollection AddEntityElements(this IServiceCollection services)
        {
            services.AddTransient<IMapper<Entity, EntityViewModel>, EntityViewModelMapper>();
            services.AddTransient<ICreateEntityPresenter, CreateEntityPresenter>();
            services.AddTransient<IGetByIdEntityPresenter, GetByIdEntityPresenter>();
            services.AddTransient<IGetEntitiesPresenter, GetEntitiesPresenter>();
            services.AddTransient<IUpdateEntityPresenter, UpdateEntityPresenter>();
            services.AddTransient<IDeleteEntityPresenter, DeleteEntityPresenter>();

            return services;
        }

        public static void UseEntityEndpoints(this WebApplication app)
        {
            app.MapCreateEntity()
                .MapGetEntity()
                .MapGetEntities()
                .MapUpdateEntity()
                .MapDeleteEntity();
        }

        private static WebApplication MapCreateEntity(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateEntityRequestModel model, IBoundary<CreateEntityRequestModel> boundary, ICreateEntityPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(EntityViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Entities");

            return app;
        }

        private static WebApplication MapGetEntity(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetEntityByIdRequestModel> boundary, IGetByIdEntityPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetEntityByIdRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(EntityViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Entities");

            return app;
        }

        private static WebApplication MapGetEntities(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetEntitiesRequestModel> boudary, IGetEntitiesPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetEntitiesRequestModel(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(EntityViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Entities");

            return app;
        }

        private static WebApplication MapUpdateEntity(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateEntityRequestModel model, IBoundary<UpdateEntityRequestModel> boundary, IUpdateEntityPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(EntityViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Entities");

            return app;
        }

        private static WebApplication MapDeleteEntity(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteEntityRequestModel> boundary, IDeleteEntityPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteEntityRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(EntityViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Entities");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
