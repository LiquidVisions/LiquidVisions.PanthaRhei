using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Components;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers
{
    public static class ComponentController
    {
        private static readonly string endpointTemplate = "/components";

        public static IServiceCollection AddComponentElements(this IServiceCollection services)
        {
            services.AddTransient<IMapper<Component, ComponentViewModel>, ComponentModelMapper>();
            services.AddTransient<ICreateComponentPresenter, CreateComponentPresenter>();
            services.AddTransient<IGetByIdComponentPresenter, GetByIdComponentPresenter>();
            services.AddTransient<IGetComponentsPresenter, GetComponentsPresenter>();
            services.AddTransient<IUpdateComponentPresenter, UpdateComponentPresenter>();
            services.AddTransient<IDeleteComponentPresenter, DeleteComponentPresenter>();

            return services;
        }

        public static void UseComponentEndpoints(this WebApplication app)
        {
            app.MapCreateComponent()
                .MapGetComponent()
                .MapGetComponents()
                .MapUpdateComponent()
                .MapDeleteComponent();
        }

        private static WebApplication MapCreateComponent(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateComponentCommand model, IBoundary<CreateComponentCommand> boundary, ICreateComponentPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(ComponentViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Components");

            return app;
        }

        private static WebApplication MapGetComponent(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetComponentByIdQuery> boundary, IGetByIdComponentPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetComponentByIdQuery { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(ComponentViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Components");

            return app;
        }

        private static WebApplication MapGetComponents(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetComponentsQuery> boudary, IGetComponentsPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetComponentsQuery(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ComponentViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Components");

            return app;
        }

        private static WebApplication MapUpdateComponent(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateComponentCommand model, IBoundary<UpdateComponentCommand> boundary, IUpdateComponentPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ComponentViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Components");

            return app;
        }

        private static WebApplication MapDeleteComponent(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteComponentCommand> boundary, IDeleteComponentPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteComponentCommand { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ComponentViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Components");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
