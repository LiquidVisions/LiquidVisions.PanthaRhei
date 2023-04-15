using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Fields;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers
{
    public static class FieldController
    {
        private static readonly string endpointTemplate = "/fields";

        public static IServiceCollection AddFieldElements(this IServiceCollection services)
        {
            services.AddTransient<IMapper<Field, FieldViewModel>, FieldViewModelMapper>();
            services.AddTransient<ICreateFieldPresenter, CreateFieldPresenter>();
            services.AddTransient<IGetByIdFieldPresenter, GetByIdFieldPresenter>();
            services.AddTransient<IGetFieldsPresenter, GetFieldsPresenter>();
            services.AddTransient<IUpdateFieldPresenter, UpdateFieldPresenter>();
            services.AddTransient<IDeleteFieldPresenter, DeleteFieldPresenter>();

            return services;
        }

        public static void UseFieldEndpoints(this WebApplication app)
        {
            app.MapCreateField()
                .MapGetField()
                .MapGetFields()
                .MapUpdateField()
                .MapDeleteField();
        }

        private static WebApplication MapCreateField(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreateFieldRequestModel model, IBoundary<CreateFieldRequestModel> boundary, ICreateFieldPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(FieldViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Fields");

            return app;
        }

        private static WebApplication MapGetField(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetFieldByIdRequestModel> boundary, IGetByIdFieldPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetFieldByIdRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(FieldViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Fields");

            return app;
        }

        private static WebApplication MapGetFields(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetFieldsRequestModel> boudary, IGetFieldsPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetFieldsRequestModel(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(FieldViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Fields");

            return app;
        }

        private static WebApplication MapUpdateField(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdateFieldRequestModel model, IBoundary<UpdateFieldRequestModel> boundary, IUpdateFieldPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(FieldViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Fields");

            return app;
        }

        private static WebApplication MapDeleteField(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeleteFieldRequestModel> boundary, IDeleteFieldPresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeleteFieldRequestModel { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(FieldViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Fields");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
