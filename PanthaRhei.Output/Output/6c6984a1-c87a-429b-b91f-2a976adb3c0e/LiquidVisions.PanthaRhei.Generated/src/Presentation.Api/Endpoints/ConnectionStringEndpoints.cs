using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Endpoints
{
    public static class ConnectionStringEndpoints
    {
        private static readonly string endpointTemplate = "/connectionstrings";

        public static IServiceCollection AddConnectionStringElements(this IServiceCollection services)
        {
            services.AddTransient<IMapper<ConnectionString, ConnectionStringViewModel>, ConnectionStringModelMapper>();
            services.AddTransient<ICreateConnectionStringPresenter, CreateConnectionStringPresenter>();
            services.AddTransient<IGetByIdConnectionStringPresenter, GetByIdConnectionStringPresenter>();
            services.AddTransient<IGetConnectionStringsPresenter, GetConnectionStringsPresenter>();
            services.AddTransient<IUpdateConnectionStringPresenter, UpdateConnectionStringPresenter>();
            services.AddTransient<IDeleteConnectionStringPresenter, DeleteConnectionStringPresenter>();

            return services;
        }

        public static void UseConnectionStringEndpoints(this WebApplication app)
        {
            app.MapCreateConnectionString()
                .MapGetConnectionString()
                .MapGetConnectionStrings()
                .MapUpdateConnectionString()
                .MapDeleteConnectionString();
        }

        private static WebApplication MapCreateConnectionString(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateConnectionStringCommand model, IBoundary<CreateConnectionStringCommand> boundary, ICreateConnectionStringPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(ConnectionStringViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("ConnectionStrings");

            return app;
        }

        private static WebApplication MapGetConnectionString(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetConnectionStringByIdQuery> boundary, IGetByIdConnectionStringPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetConnectionStringByIdQuery { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(ConnectionStringViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("ConnectionStrings");

            return app;
        }

        private static WebApplication MapGetConnectionStrings(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetConnectionStringsQuery> boudary, IGetConnectionStringsPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetConnectionStringsQuery(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ConnectionStringViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("ConnectionStrings");

            return app;
        }

        private static WebApplication MapUpdateConnectionString(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateConnectionStringCommand model, IBoundary<UpdateConnectionStringCommand> boundary, IUpdateConnectionStringPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ConnectionStringViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("ConnectionStrings");

            return app;
        }

        private static WebApplication MapDeleteConnectionString(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteConnectionStringCommand> boundary, IDeleteConnectionStringPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteConnectionStringCommand { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ConnectionStringViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("ConnectionStrings");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
