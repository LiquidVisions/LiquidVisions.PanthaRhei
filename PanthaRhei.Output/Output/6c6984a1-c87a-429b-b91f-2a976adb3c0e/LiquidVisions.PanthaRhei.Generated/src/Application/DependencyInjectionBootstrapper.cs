using Microsoft.Extensions.DependencyInjection;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Components;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Application
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            return services;
        }

































































































        private static IServiceCollection AddField(this IServiceCollection services)
        {
            // CREATE FIELD
            services.AddTransient<IInteractor<CreateFieldRequestModel>, CreateFieldInteractor>();
            services.AddTransient<IBoundary<CreateFieldRequestModel>, CreateFieldBoundary>();
            services.AddTransient<IValidator<CreateFieldRequestModel>, CreateFieldValidator>();

            // GET 
            services.AddTransient<IInteractor<GetFieldsRequestModel>, GetFieldsInteractor>();
            services.AddTransient<IBoundary<GetFieldsRequestModel>, GetFieldsBoundary>();
            services.AddTransient<IValidator<GetFieldsRequestModel>, GetFieldsValidator>();

            // GET FIELD
            services.AddTransient<IInteractor<GetFieldByIdRequestModel>, GetFieldInteractor>();
            services.AddTransient<IBoundary<GetFieldByIdRequestModel>, GetFieldByIdBoundary>();
            services.AddTransient<IValidator<GetFieldByIdRequestModel>, GetFieldByIdValidator>();

            // DELETE FIELD
            services.AddTransient<IInteractor<DeleteFieldRequestModel>, DeleteFieldInteractor>();
            services.AddTransient<IBoundary<DeleteFieldRequestModel>, DeleteFieldBoundary>();
            services.AddTransient<IValidator<DeleteFieldRequestModel>, DeleteFieldValidator>();

            // UPDATE FIELD
            services.AddTransient<IInteractor<UpdateFieldRequestModel>, UpdateFieldInteractor>();
            services.AddTransient<IBoundary<UpdateFieldRequestModel>, UpdateFieldBoundary>();
            services.AddTransient<IValidator<UpdateFieldRequestModel>, UpdateFieldValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateFieldRequestModel, Field>, CreateFieldRequestModelMapper>();
            services.AddTransient<IMapper<UpdateFieldRequestModel, Field>, UpdateFieldRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddApp(this IServiceCollection services)
        {
            // CREATE APP
            services.AddTransient<IInteractor<CreateAppRequestModel>, CreateAppInteractor>();
            services.AddTransient<IBoundary<CreateAppRequestModel>, CreateAppBoundary>();
            services.AddTransient<IValidator<CreateAppRequestModel>, CreateAppValidator>();

            // GET 
            services.AddTransient<IInteractor<GetAppsRequestModel>, GetAppsInteractor>();
            services.AddTransient<IBoundary<GetAppsRequestModel>, GetAppsBoundary>();
            services.AddTransient<IValidator<GetAppsRequestModel>, GetAppsValidator>();

            // GET APP
            services.AddTransient<IInteractor<GetAppByIdRequestModel>, GetAppInteractor>();
            services.AddTransient<IBoundary<GetAppByIdRequestModel>, GetAppByIdBoundary>();
            services.AddTransient<IValidator<GetAppByIdRequestModel>, GetAppByIdValidator>();

            // DELETE APP
            services.AddTransient<IInteractor<DeleteAppRequestModel>, DeleteAppInteractor>();
            services.AddTransient<IBoundary<DeleteAppRequestModel>, DeleteAppBoundary>();
            services.AddTransient<IValidator<DeleteAppRequestModel>, DeleteAppValidator>();

            // UPDATE APP
            services.AddTransient<IInteractor<UpdateAppRequestModel>, UpdateAppInteractor>();
            services.AddTransient<IBoundary<UpdateAppRequestModel>, UpdateAppBoundary>();
            services.AddTransient<IValidator<UpdateAppRequestModel>, UpdateAppValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateAppRequestModel, App>, CreateAppRequestModelMapper>();
            services.AddTransient<IMapper<UpdateAppRequestModel, App>, UpdateAppRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddPackage(this IServiceCollection services)
        {
            // CREATE PACKAGE
            services.AddTransient<IInteractor<CreatePackageRequestModel>, CreatePackageInteractor>();
            services.AddTransient<IBoundary<CreatePackageRequestModel>, CreatePackageBoundary>();
            services.AddTransient<IValidator<CreatePackageRequestModel>, CreatePackageValidator>();

            // GET 
            services.AddTransient<IInteractor<GetPackagesRequestModel>, GetPackagesInteractor>();
            services.AddTransient<IBoundary<GetPackagesRequestModel>, GetPackagesBoundary>();
            services.AddTransient<IValidator<GetPackagesRequestModel>, GetPackagesValidator>();

            // GET PACKAGE
            services.AddTransient<IInteractor<GetPackageByIdRequestModel>, GetPackageInteractor>();
            services.AddTransient<IBoundary<GetPackageByIdRequestModel>, GetPackageByIdBoundary>();
            services.AddTransient<IValidator<GetPackageByIdRequestModel>, GetPackageByIdValidator>();

            // DELETE PACKAGE
            services.AddTransient<IInteractor<DeletePackageRequestModel>, DeletePackageInteractor>();
            services.AddTransient<IBoundary<DeletePackageRequestModel>, DeletePackageBoundary>();
            services.AddTransient<IValidator<DeletePackageRequestModel>, DeletePackageValidator>();

            // UPDATE PACKAGE
            services.AddTransient<IInteractor<UpdatePackageRequestModel>, UpdatePackageInteractor>();
            services.AddTransient<IBoundary<UpdatePackageRequestModel>, UpdatePackageBoundary>();
            services.AddTransient<IValidator<UpdatePackageRequestModel>, UpdatePackageValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreatePackageRequestModel, Package>, CreatePackageRequestModelMapper>();
            services.AddTransient<IMapper<UpdatePackageRequestModel, Package>, UpdatePackageRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddEntity(this IServiceCollection services)
        {
            // CREATE ENTITY
            services.AddTransient<IInteractor<CreateEntityRequestModel>, CreateEntityInteractor>();
            services.AddTransient<IBoundary<CreateEntityRequestModel>, CreateEntityBoundary>();
            services.AddTransient<IValidator<CreateEntityRequestModel>, CreateEntityValidator>();

            // GET 
            services.AddTransient<IInteractor<GetEntitiesRequestModel>, GetEntitiesInteractor>();
            services.AddTransient<IBoundary<GetEntitiesRequestModel>, GetEntitiesBoundary>();
            services.AddTransient<IValidator<GetEntitiesRequestModel>, GetEntitiesValidator>();

            // GET ENTITY
            services.AddTransient<IInteractor<GetEntityByIdRequestModel>, GetEntityInteractor>();
            services.AddTransient<IBoundary<GetEntityByIdRequestModel>, GetEntityByIdBoundary>();
            services.AddTransient<IValidator<GetEntityByIdRequestModel>, GetEntityByIdValidator>();

            // DELETE ENTITY
            services.AddTransient<IInteractor<DeleteEntityRequestModel>, DeleteEntityInteractor>();
            services.AddTransient<IBoundary<DeleteEntityRequestModel>, DeleteEntityBoundary>();
            services.AddTransient<IValidator<DeleteEntityRequestModel>, DeleteEntityValidator>();

            // UPDATE ENTITY
            services.AddTransient<IInteractor<UpdateEntityRequestModel>, UpdateEntityInteractor>();
            services.AddTransient<IBoundary<UpdateEntityRequestModel>, UpdateEntityBoundary>();
            services.AddTransient<IValidator<UpdateEntityRequestModel>, UpdateEntityValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateEntityRequestModel, Entity>, CreateEntityRequestModelMapper>();
            services.AddTransient<IMapper<UpdateEntityRequestModel, Entity>, UpdateEntityRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddComponent(this IServiceCollection services)
        {
            // CREATE COMPONENT
            services.AddTransient<IInteractor<CreateComponentRequestModel>, CreateComponentInteractor>();
            services.AddTransient<IBoundary<CreateComponentRequestModel>, CreateComponentBoundary>();
            services.AddTransient<IValidator<CreateComponentRequestModel>, CreateComponentValidator>();

            // GET 
            services.AddTransient<IInteractor<GetComponentsRequestModel>, GetComponentsInteractor>();
            services.AddTransient<IBoundary<GetComponentsRequestModel>, GetComponentsBoundary>();
            services.AddTransient<IValidator<GetComponentsRequestModel>, GetComponentsValidator>();

            // GET COMPONENT
            services.AddTransient<IInteractor<GetComponentByIdRequestModel>, GetComponentInteractor>();
            services.AddTransient<IBoundary<GetComponentByIdRequestModel>, GetComponentByIdBoundary>();
            services.AddTransient<IValidator<GetComponentByIdRequestModel>, GetComponentByIdValidator>();

            // DELETE COMPONENT
            services.AddTransient<IInteractor<DeleteComponentRequestModel>, DeleteComponentInteractor>();
            services.AddTransient<IBoundary<DeleteComponentRequestModel>, DeleteComponentBoundary>();
            services.AddTransient<IValidator<DeleteComponentRequestModel>, DeleteComponentValidator>();

            // UPDATE COMPONENT
            services.AddTransient<IInteractor<UpdateComponentRequestModel>, UpdateComponentInteractor>();
            services.AddTransient<IBoundary<UpdateComponentRequestModel>, UpdateComponentBoundary>();
            services.AddTransient<IValidator<UpdateComponentRequestModel>, UpdateComponentValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateComponentRequestModel, Component>, CreateComponentRequestModelMapper>();
            services.AddTransient<IMapper<UpdateComponentRequestModel, Component>, UpdateComponentRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddExpander(this IServiceCollection services)
        {
            // CREATE EXPANDER
            services.AddTransient<IInteractor<CreateExpanderRequestModel>, CreateExpanderInteractor>();
            services.AddTransient<IBoundary<CreateExpanderRequestModel>, CreateExpanderBoundary>();
            services.AddTransient<IValidator<CreateExpanderRequestModel>, CreateExpanderValidator>();

            // GET 
            services.AddTransient<IInteractor<GetExpandersRequestModel>, GetExpandersInteractor>();
            services.AddTransient<IBoundary<GetExpandersRequestModel>, GetExpandersBoundary>();
            services.AddTransient<IValidator<GetExpandersRequestModel>, GetExpandersValidator>();

            // GET EXPANDER
            services.AddTransient<IInteractor<GetExpanderByIdRequestModel>, GetExpanderInteractor>();
            services.AddTransient<IBoundary<GetExpanderByIdRequestModel>, GetExpanderByIdBoundary>();
            services.AddTransient<IValidator<GetExpanderByIdRequestModel>, GetExpanderByIdValidator>();

            // DELETE EXPANDER
            services.AddTransient<IInteractor<DeleteExpanderRequestModel>, DeleteExpanderInteractor>();
            services.AddTransient<IBoundary<DeleteExpanderRequestModel>, DeleteExpanderBoundary>();
            services.AddTransient<IValidator<DeleteExpanderRequestModel>, DeleteExpanderValidator>();

            // UPDATE EXPANDER
            services.AddTransient<IInteractor<UpdateExpanderRequestModel>, UpdateExpanderInteractor>();
            services.AddTransient<IBoundary<UpdateExpanderRequestModel>, UpdateExpanderBoundary>();
            services.AddTransient<IValidator<UpdateExpanderRequestModel>, UpdateExpanderValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateExpanderRequestModel, Expander>, CreateExpanderRequestModelMapper>();
            services.AddTransient<IMapper<UpdateExpanderRequestModel, Expander>, UpdateExpanderRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddConnectionString(this IServiceCollection services)
        {
            // CREATE CONNECTIONSTRING
            services.AddTransient<IInteractor<CreateConnectionStringRequestModel>, CreateConnectionStringInteractor>();
            services.AddTransient<IBoundary<CreateConnectionStringRequestModel>, CreateConnectionStringBoundary>();
            services.AddTransient<IValidator<CreateConnectionStringRequestModel>, CreateConnectionStringValidator>();

            // GET 
            services.AddTransient<IInteractor<GetConnectionStringsRequestModel>, GetConnectionStringsInteractor>();
            services.AddTransient<IBoundary<GetConnectionStringsRequestModel>, GetConnectionStringsBoundary>();
            services.AddTransient<IValidator<GetConnectionStringsRequestModel>, GetConnectionStringsValidator>();

            // GET CONNECTIONSTRING
            services.AddTransient<IInteractor<GetConnectionStringByIdRequestModel>, GetConnectionStringInteractor>();
            services.AddTransient<IBoundary<GetConnectionStringByIdRequestModel>, GetConnectionStringByIdBoundary>();
            services.AddTransient<IValidator<GetConnectionStringByIdRequestModel>, GetConnectionStringByIdValidator>();

            // DELETE CONNECTIONSTRING
            services.AddTransient<IInteractor<DeleteConnectionStringRequestModel>, DeleteConnectionStringInteractor>();
            services.AddTransient<IBoundary<DeleteConnectionStringRequestModel>, DeleteConnectionStringBoundary>();
            services.AddTransient<IValidator<DeleteConnectionStringRequestModel>, DeleteConnectionStringValidator>();

            // UPDATE CONNECTIONSTRING
            services.AddTransient<IInteractor<UpdateConnectionStringRequestModel>, UpdateConnectionStringInteractor>();
            services.AddTransient<IBoundary<UpdateConnectionStringRequestModel>, UpdateConnectionStringBoundary>();
            services.AddTransient<IValidator<UpdateConnectionStringRequestModel>, UpdateConnectionStringValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateConnectionStringRequestModel, ConnectionString>, CreateConnectionStringRequestModelMapper>();
            services.AddTransient<IMapper<UpdateConnectionStringRequestModel, ConnectionString>, UpdateConnectionStringRequestModelMapper>();

            return services;
        }

        private static IServiceCollection AddRelationship(this IServiceCollection services)
        {
            // CREATE RELATIONSHIP
            services.AddTransient<IInteractor<CreateRelationshipRequestModel>, CreateRelationshipInteractor>();
            services.AddTransient<IBoundary<CreateRelationshipRequestModel>, CreateRelationshipBoundary>();
            services.AddTransient<IValidator<CreateRelationshipRequestModel>, CreateRelationshipValidator>();

            // GET 
            services.AddTransient<IInteractor<GetRelationshipsRequestModel>, GetRelationshipsInteractor>();
            services.AddTransient<IBoundary<GetRelationshipsRequestModel>, GetRelationshipsBoundary>();
            services.AddTransient<IValidator<GetRelationshipsRequestModel>, GetRelationshipsValidator>();

            // GET RELATIONSHIP
            services.AddTransient<IInteractor<GetRelationshipByIdRequestModel>, GetRelationshipInteractor>();
            services.AddTransient<IBoundary<GetRelationshipByIdRequestModel>, GetRelationshipByIdBoundary>();
            services.AddTransient<IValidator<GetRelationshipByIdRequestModel>, GetRelationshipByIdValidator>();

            // DELETE RELATIONSHIP
            services.AddTransient<IInteractor<DeleteRelationshipRequestModel>, DeleteRelationshipInteractor>();
            services.AddTransient<IBoundary<DeleteRelationshipRequestModel>, DeleteRelationshipBoundary>();
            services.AddTransient<IValidator<DeleteRelationshipRequestModel>, DeleteRelationshipValidator>();

            // UPDATE RELATIONSHIP
            services.AddTransient<IInteractor<UpdateRelationshipRequestModel>, UpdateRelationshipInteractor>();
            services.AddTransient<IBoundary<UpdateRelationshipRequestModel>, UpdateRelationshipBoundary>();
            services.AddTransient<IValidator<UpdateRelationshipRequestModel>, UpdateRelationshipValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateRelationshipRequestModel, Relationship>, CreateRelationshipRequestModelMapper>();
            services.AddTransient<IMapper<UpdateRelationshipRequestModel, Relationship>, UpdateRelationshipRequestModelMapper>();

            return services;
        }
    }
}
