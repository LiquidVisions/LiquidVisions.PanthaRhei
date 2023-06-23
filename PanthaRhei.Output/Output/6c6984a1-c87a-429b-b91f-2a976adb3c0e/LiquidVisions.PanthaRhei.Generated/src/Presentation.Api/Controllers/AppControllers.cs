using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Apps;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers
{
    public static class AppController
    {
        private static readonly string endpointTemplate = "/apps";

        public static IServiceCollection AddAppElements(this IServiceCollection services)
        {
            //services.AddTransient<IMapper<App, AppViewModel>, AppViewModelMapper>();
            services.AddTransient<ICreateAppPresenter, CreateAppPresenter>();
            services.AddTransient<IGetByIdAppPresenter, GetByIdAppPresenter>();
            services.AddTransient<IGetAppsPresenter, GetAppsPresenter>();
            services.AddTransient<IUpdateAppPresenter, UpdateAppPresenter>();
            services.AddTransient<IDeleteAppPresenter, DeleteAppPresenter>();

            return services;
        }

        public static void UseAppEndpoints(this WebApplication app)
        {
            app.MapCreateApp()
                .MapGetApp()
                .MapGetApps()
                .MapUpdateApp()
                .MapDeleteApp();
        }

        private static WebApplication MapCreateApp(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateAppRequestModel model, IBoundary<CreateAppRequestModel> boundary, ICreateAppPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(AppViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Apps");

            return app;
        }

        private static WebApplication MapGetApp(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetAppByIdRequestModel> boundary, IGetByIdAppPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetAppByIdRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(AppViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Apps");

            return app;
        }

        private static WebApplication MapGetApps(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetAppsRequestModel> boudary, IGetAppsPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetAppsRequestModel(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(AppViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Apps");

            return app;
        }

        private static WebApplication MapUpdateApp(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateAppRequestModel model, IBoundary<UpdateAppRequestModel> boundary, IUpdateAppPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(AppViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Apps");

            return app;
        }

        private static WebApplication MapDeleteApp(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteAppRequestModel> boundary, IDeleteAppPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteAppRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(AppViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Apps");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
