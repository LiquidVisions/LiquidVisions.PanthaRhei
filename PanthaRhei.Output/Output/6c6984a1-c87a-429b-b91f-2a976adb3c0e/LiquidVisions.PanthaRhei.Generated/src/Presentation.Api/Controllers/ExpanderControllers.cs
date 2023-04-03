using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Expanders;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers
{
    public static class ExpanderController
    {
        private static readonly string endpointTemplate = "/expanders";

        public static IServiceCollection AddExpanderElements(this IServiceCollection services)
        {
            services.AddTransient<IMapper<Expander, ExpanderViewModel>, ExpanderModelMapper>();
            services.AddTransient<ICreateExpanderPresenter, CreateExpanderPresenter>();
            services.AddTransient<IGetByIdExpanderPresenter, GetByIdExpanderPresenter>();
            services.AddTransient<IGetExpandersPresenter, GetExpandersPresenter>();
            services.AddTransient<IUpdateExpanderPresenter, UpdateExpanderPresenter>();
            services.AddTransient<IDeleteExpanderPresenter, DeleteExpanderPresenter>();

            return services;
        }

        public static void UseExpanderEndpoints(this WebApplication app)
        {
            app.MapCreateExpander()
                .MapGetExpander()
                .MapGetExpanders()
                .MapUpdateExpander()
                .MapDeleteExpander();
        }

        private static WebApplication MapCreateExpander(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateExpanderCommand model, IBoundary<CreateExpanderCommand> boundary, ICreateExpanderPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(ExpanderViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Expanders");

            return app;
        }

        private static WebApplication MapGetExpander(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetExpanderByIdQuery> boundary, IGetByIdExpanderPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetExpanderByIdQuery { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(ExpanderViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Expanders");

            return app;
        }

        private static WebApplication MapGetExpanders(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetExpandersQuery> boudary, IGetExpandersPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetExpandersQuery(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ExpanderViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Expanders");

            return app;
        }

        private static WebApplication MapUpdateExpander(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateExpanderCommand model, IBoundary<UpdateExpanderCommand> boundary, IUpdateExpanderPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ExpanderViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Expanders");

            return app;
        }

        private static WebApplication MapDeleteExpander(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteExpanderCommand> boundary, IDeleteExpanderPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteExpanderCommand { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(ExpanderViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Expanders");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
