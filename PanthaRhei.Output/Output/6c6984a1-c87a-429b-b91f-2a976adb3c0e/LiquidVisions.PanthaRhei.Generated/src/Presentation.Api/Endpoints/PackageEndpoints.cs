using System;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers;
using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters.Packages;
using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Endpoints
{
    public static class PackageEndpoints
    {
        private static readonly string endpointTemplate = "/packages";

        public static IServiceCollection AddPackageElements(this IServiceCollection services)
        {
            services.AddTransient<IMapper<Package, PackageViewModel>, PackageModelMapper>();
            services.AddTransient<ICreatePackagePresenter, CreatePackagePresenter>();
            services.AddTransient<IGetByIdPackagePresenter, GetByIdPackagePresenter>();
            services.AddTransient<IGetPackagesPresenter, GetPackagesPresenter>();
            services.AddTransient<IUpdatePackagePresenter, UpdatePackagePresenter>();
            services.AddTransient<IDeletePackagePresenter, DeletePackagePresenter>();

            return services;
        }

        public static void UsePackageEndpoints(this WebApplication app)
        {
            app.MapCreatePackage()
                .MapGetPackage()
                .MapGetPackages()
                .MapUpdatePackage()
                .MapDeletePackage();
        }

        private static WebApplication MapCreatePackage(this WebApplication app)
        {
            RouteHandlerBuilder builder =  app.MapPost(endpointTemplate, async (CreatePackageCommand model, IBoundary<CreatePackageCommand> boundary, ICreatePackagePresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status201Created, typeof(PackageViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Packages");

            return app;
        }

        private static WebApplication MapGetPackage(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<GetPackageByIdQuery> boundary, IGetByIdPackagePresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new GetPackageByIdQuery { Id = id }, presenter);
                return presenter.GetResult(request);
            });
            
            builder.Produces(StatusCodes.Status200OK, typeof(PackageViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Packages");

            return app;
        }

        private static WebApplication MapGetPackages(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapGet(endpointTemplate, async (IBoundary<GetPackagesQuery> boudary, IGetPackagesPresenter presenter, HttpRequest request) =>
            {
                await boudary.Execute(new GetPackagesQuery(), presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(PackageViewModel[]));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Packages");

            return app;
        }

        private static WebApplication MapUpdatePackage(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapPut(endpointTemplate, async (UpdatePackageCommand model, IBoundary<UpdatePackageCommand> boundary, IUpdatePackagePresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(model, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(PackageViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.WithTags("Packages");

            return app;
        }

        private static WebApplication MapDeletePackage(this WebApplication app)
        {
            RouteHandlerBuilder builder = app.MapDelete($"{endpointTemplate}/{{id:Guid}}", async (Guid id, IBoundary<DeletePackageCommand> boundary, IDeletePackagePresenter presenter, HttpRequest request) =>
            {
                await boundary.Execute(new DeletePackageCommand { Id = id }, presenter);
                return presenter.GetResult(request);
            });

            builder.Produces(StatusCodes.Status200OK, typeof(PackageViewModel));
            builder.Produces(StatusCodes.Status500InternalServerError, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status400BadRequest, typeof(ErrorViewModel));
            builder.Produces(StatusCodes.Status404NotFound, typeof(ErrorViewModel));
            builder.WithTags("Packages");

            return app;
        }

        #region ns-custom-endpoint
        #endregion ns-custom-endpoint
    }
}
