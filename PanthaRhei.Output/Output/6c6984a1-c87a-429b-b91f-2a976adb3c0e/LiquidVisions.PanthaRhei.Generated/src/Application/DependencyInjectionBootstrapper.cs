using Microsoft.Extensions.DependencyInjection;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Fields;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Apps;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Packages;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Packages;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Entities;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Components;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Expanders;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers.Relationships;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Application
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            DependencyInjectionContainer serviceProvider = new DependencyInjectionContainer(services);
            services.AddSingleton<IDependencyServiceProvider>(serviceProvider);

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
            services.AddTransient<IInteractor<CreateFieldCommand>, CreateFieldInteractor>();
            services.AddTransient<IBoundary<CreateFieldCommand>, CreateFieldBoundary>();
            services.AddTransient<IValidator<CreateFieldCommand>, CreateFieldValidator>();

            // GET 
            services.AddTransient<IInteractor<GetFieldsQuery>, GetFieldsInteractor>();
            services.AddTransient<IBoundary<GetFieldsQuery>, GetFieldsBoundary>();
            services.AddTransient<IValidator<GetFieldsQuery>, GetFieldsValidator>();

            // GET FIELD
            services.AddTransient<IInteractor<GetFieldByIdQuery>, GetFieldInteractor>();
            services.AddTransient<IBoundary<GetFieldByIdQuery>, GetFieldByIdBoundary>();
            services.AddTransient<IValidator<GetFieldByIdQuery>, GetFieldByIdValidator>();

            // DELETE FIELD
            services.AddTransient<IInteractor<DeleteFieldCommand>, DeleteFieldInteractor>();
            services.AddTransient<IBoundary<DeleteFieldCommand>, DeleteFieldBoundary>();
            services.AddTransient<IValidator<DeleteFieldCommand>, DeleteFieldValidator>();

            // UPDATE FIELD
            services.AddTransient<IInteractor<UpdateFieldCommand>, UpdateFieldInteractor>();
            services.AddTransient<IBoundary<UpdateFieldCommand>, UpdateFieldBoundary>();
            services.AddTransient<IValidator<UpdateFieldCommand>, UpdateFieldValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateFieldCommand, Field>, CreateFieldCommandToFieldMapper>();
            services.AddTransient<IMapper<UpdateFieldCommand, Field>, UpdateFieldCommandToFieldMapper>();

            return services;
        }

        private static IServiceCollection AddApp(this IServiceCollection services)
        {
            // CREATE APP
            services.AddTransient<IInteractor<CreateAppCommand>, CreateAppInteractor>();
            services.AddTransient<IBoundary<CreateAppCommand>, CreateAppBoundary>();
            services.AddTransient<IValidator<CreateAppCommand>, CreateAppValidator>();

            // GET 
            services.AddTransient<IInteractor<GetAppsQuery>, GetAppsInteractor>();
            services.AddTransient<IBoundary<GetAppsQuery>, GetAppsBoundary>();
            services.AddTransient<IValidator<GetAppsQuery>, GetAppsValidator>();

            // GET APP
            services.AddTransient<IInteractor<GetAppByIdQuery>, GetAppInteractor>();
            services.AddTransient<IBoundary<GetAppByIdQuery>, GetAppByIdBoundary>();
            services.AddTransient<IValidator<GetAppByIdQuery>, GetAppByIdValidator>();

            // DELETE APP
            services.AddTransient<IInteractor<DeleteAppCommand>, DeleteAppInteractor>();
            services.AddTransient<IBoundary<DeleteAppCommand>, DeleteAppBoundary>();
            services.AddTransient<IValidator<DeleteAppCommand>, DeleteAppValidator>();

            // UPDATE APP
            services.AddTransient<IInteractor<UpdateAppCommand>, UpdateAppInteractor>();
            services.AddTransient<IBoundary<UpdateAppCommand>, UpdateAppBoundary>();
            services.AddTransient<IValidator<UpdateAppCommand>, UpdateAppValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateAppCommand, App>, CreateAppCommandToAppMapper>();
            services.AddTransient<IMapper<UpdateAppCommand, App>, UpdateAppCommandToAppMapper>();

            return services;
        }

        private static IServiceCollection AddPackage(this IServiceCollection services)
        {
            // CREATE PACKAGE
            services.AddTransient<IInteractor<CreatePackageCommand>, CreatePackageInteractor>();
            services.AddTransient<IBoundary<CreatePackageCommand>, CreatePackageBoundary>();
            services.AddTransient<IValidator<CreatePackageCommand>, CreatePackageValidator>();

            // GET 
            services.AddTransient<IInteractor<GetPackagesQuery>, GetPackagesInteractor>();
            services.AddTransient<IBoundary<GetPackagesQuery>, GetPackagesBoundary>();
            services.AddTransient<IValidator<GetPackagesQuery>, GetPackagesValidator>();

            // GET PACKAGE
            services.AddTransient<IInteractor<GetPackageByIdQuery>, GetPackageInteractor>();
            services.AddTransient<IBoundary<GetPackageByIdQuery>, GetPackageByIdBoundary>();
            services.AddTransient<IValidator<GetPackageByIdQuery>, GetPackageByIdValidator>();

            // DELETE PACKAGE
            services.AddTransient<IInteractor<DeletePackageCommand>, DeletePackageInteractor>();
            services.AddTransient<IBoundary<DeletePackageCommand>, DeletePackageBoundary>();
            services.AddTransient<IValidator<DeletePackageCommand>, DeletePackageValidator>();

            // UPDATE PACKAGE
            services.AddTransient<IInteractor<UpdatePackageCommand>, UpdatePackageInteractor>();
            services.AddTransient<IBoundary<UpdatePackageCommand>, UpdatePackageBoundary>();
            services.AddTransient<IValidator<UpdatePackageCommand>, UpdatePackageValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreatePackageCommand, Package>, CreatePackageCommandToPackageMapper>();
            services.AddTransient<IMapper<UpdatePackageCommand, Package>, UpdatePackageCommandToPackageMapper>();

            return services;
        }

        private static IServiceCollection AddEntity(this IServiceCollection services)
        {
            // CREATE ENTITY
            services.AddTransient<IInteractor<CreateEntityCommand>, CreateEntityInteractor>();
            services.AddTransient<IBoundary<CreateEntityCommand>, CreateEntityBoundary>();
            services.AddTransient<IValidator<CreateEntityCommand>, CreateEntityValidator>();

            // GET 
            services.AddTransient<IInteractor<GetEntitiesQuery>, GetEntitiesInteractor>();
            services.AddTransient<IBoundary<GetEntitiesQuery>, GetEntitiesBoundary>();
            services.AddTransient<IValidator<GetEntitiesQuery>, GetEntitiesValidator>();

            // GET ENTITY
            services.AddTransient<IInteractor<GetEntityByIdQuery>, GetEntityInteractor>();
            services.AddTransient<IBoundary<GetEntityByIdQuery>, GetEntityByIdBoundary>();
            services.AddTransient<IValidator<GetEntityByIdQuery>, GetEntityByIdValidator>();

            // DELETE ENTITY
            services.AddTransient<IInteractor<DeleteEntityCommand>, DeleteEntityInteractor>();
            services.AddTransient<IBoundary<DeleteEntityCommand>, DeleteEntityBoundary>();
            services.AddTransient<IValidator<DeleteEntityCommand>, DeleteEntityValidator>();

            // UPDATE ENTITY
            services.AddTransient<IInteractor<UpdateEntityCommand>, UpdateEntityInteractor>();
            services.AddTransient<IBoundary<UpdateEntityCommand>, UpdateEntityBoundary>();
            services.AddTransient<IValidator<UpdateEntityCommand>, UpdateEntityValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateEntityCommand, Entity>, CreateEntityCommandToEntityMapper>();
            services.AddTransient<IMapper<UpdateEntityCommand, Entity>, UpdateEntityCommandToEntityMapper>();

            return services;
        }

        private static IServiceCollection AddComponent(this IServiceCollection services)
        {
            // CREATE COMPONENT
            services.AddTransient<IInteractor<CreateComponentCommand>, CreateComponentInteractor>();
            services.AddTransient<IBoundary<CreateComponentCommand>, CreateComponentBoundary>();
            services.AddTransient<IValidator<CreateComponentCommand>, CreateComponentValidator>();

            // GET 
            services.AddTransient<IInteractor<GetComponentsQuery>, GetComponentsInteractor>();
            services.AddTransient<IBoundary<GetComponentsQuery>, GetComponentsBoundary>();
            services.AddTransient<IValidator<GetComponentsQuery>, GetComponentsValidator>();

            // GET COMPONENT
            services.AddTransient<IInteractor<GetComponentByIdQuery>, GetComponentInteractor>();
            services.AddTransient<IBoundary<GetComponentByIdQuery>, GetComponentByIdBoundary>();
            services.AddTransient<IValidator<GetComponentByIdQuery>, GetComponentByIdValidator>();

            // DELETE COMPONENT
            services.AddTransient<IInteractor<DeleteComponentCommand>, DeleteComponentInteractor>();
            services.AddTransient<IBoundary<DeleteComponentCommand>, DeleteComponentBoundary>();
            services.AddTransient<IValidator<DeleteComponentCommand>, DeleteComponentValidator>();

            // UPDATE COMPONENT
            services.AddTransient<IInteractor<UpdateComponentCommand>, UpdateComponentInteractor>();
            services.AddTransient<IBoundary<UpdateComponentCommand>, UpdateComponentBoundary>();
            services.AddTransient<IValidator<UpdateComponentCommand>, UpdateComponentValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateComponentCommand, Component>, CreateComponentCommandToComponentMapper>();
            services.AddTransient<IMapper<UpdateComponentCommand, Component>, UpdateComponentCommandToComponentMapper>();

            return services;
        }

        private static IServiceCollection AddExpander(this IServiceCollection services)
        {
            // CREATE EXPANDER
            services.AddTransient<IInteractor<CreateExpanderCommand>, CreateExpanderInteractor>();
            services.AddTransient<IBoundary<CreateExpanderCommand>, CreateExpanderBoundary>();
            services.AddTransient<IValidator<CreateExpanderCommand>, CreateExpanderValidator>();

            // GET 
            services.AddTransient<IInteractor<GetExpandersQuery>, GetExpandersInteractor>();
            services.AddTransient<IBoundary<GetExpandersQuery>, GetExpandersBoundary>();
            services.AddTransient<IValidator<GetExpandersQuery>, GetExpandersValidator>();

            // GET EXPANDER
            services.AddTransient<IInteractor<GetExpanderByIdQuery>, GetExpanderInteractor>();
            services.AddTransient<IBoundary<GetExpanderByIdQuery>, GetExpanderByIdBoundary>();
            services.AddTransient<IValidator<GetExpanderByIdQuery>, GetExpanderByIdValidator>();

            // DELETE EXPANDER
            services.AddTransient<IInteractor<DeleteExpanderCommand>, DeleteExpanderInteractor>();
            services.AddTransient<IBoundary<DeleteExpanderCommand>, DeleteExpanderBoundary>();
            services.AddTransient<IValidator<DeleteExpanderCommand>, DeleteExpanderValidator>();

            // UPDATE EXPANDER
            services.AddTransient<IInteractor<UpdateExpanderCommand>, UpdateExpanderInteractor>();
            services.AddTransient<IBoundary<UpdateExpanderCommand>, UpdateExpanderBoundary>();
            services.AddTransient<IValidator<UpdateExpanderCommand>, UpdateExpanderValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateExpanderCommand, Expander>, CreateExpanderCommandToExpanderMapper>();
            services.AddTransient<IMapper<UpdateExpanderCommand, Expander>, UpdateExpanderCommandToExpanderMapper>();

            return services;
        }

        private static IServiceCollection AddConnectionString(this IServiceCollection services)
        {
            // CREATE CONNECTIONSTRING
            services.AddTransient<IInteractor<CreateConnectionStringCommand>, CreateConnectionStringInteractor>();
            services.AddTransient<IBoundary<CreateConnectionStringCommand>, CreateConnectionStringBoundary>();
            services.AddTransient<IValidator<CreateConnectionStringCommand>, CreateConnectionStringValidator>();

            // GET 
            services.AddTransient<IInteractor<GetConnectionStringsQuery>, GetConnectionStringsInteractor>();
            services.AddTransient<IBoundary<GetConnectionStringsQuery>, GetConnectionStringsBoundary>();
            services.AddTransient<IValidator<GetConnectionStringsQuery>, GetConnectionStringsValidator>();

            // GET CONNECTIONSTRING
            services.AddTransient<IInteractor<GetConnectionStringByIdQuery>, GetConnectionStringInteractor>();
            services.AddTransient<IBoundary<GetConnectionStringByIdQuery>, GetConnectionStringByIdBoundary>();
            services.AddTransient<IValidator<GetConnectionStringByIdQuery>, GetConnectionStringByIdValidator>();

            // DELETE CONNECTIONSTRING
            services.AddTransient<IInteractor<DeleteConnectionStringCommand>, DeleteConnectionStringInteractor>();
            services.AddTransient<IBoundary<DeleteConnectionStringCommand>, DeleteConnectionStringBoundary>();
            services.AddTransient<IValidator<DeleteConnectionStringCommand>, DeleteConnectionStringValidator>();

            // UPDATE CONNECTIONSTRING
            services.AddTransient<IInteractor<UpdateConnectionStringCommand>, UpdateConnectionStringInteractor>();
            services.AddTransient<IBoundary<UpdateConnectionStringCommand>, UpdateConnectionStringBoundary>();
            services.AddTransient<IValidator<UpdateConnectionStringCommand>, UpdateConnectionStringValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateConnectionStringCommand, ConnectionString>, CreateConnectionStringCommandToConnectionStringMapper>();
            services.AddTransient<IMapper<UpdateConnectionStringCommand, ConnectionString>, UpdateConnectionStringCommandToConnectionStringMapper>();

            return services;
        }

        private static IServiceCollection AddRelationship(this IServiceCollection services)
        {
            // CREATE RELATIONSHIP
            services.AddTransient<IInteractor<CreateRelationshipCommand>, CreateRelationshipInteractor>();
            services.AddTransient<IBoundary<CreateRelationshipCommand>, CreateRelationshipBoundary>();
            services.AddTransient<IValidator<CreateRelationshipCommand>, CreateRelationshipValidator>();

            // GET 
            services.AddTransient<IInteractor<GetRelationshipsQuery>, GetRelationshipsInteractor>();
            services.AddTransient<IBoundary<GetRelationshipsQuery>, GetRelationshipsBoundary>();
            services.AddTransient<IValidator<GetRelationshipsQuery>, GetRelationshipsValidator>();

            // GET RELATIONSHIP
            services.AddTransient<IInteractor<GetRelationshipByIdQuery>, GetRelationshipInteractor>();
            services.AddTransient<IBoundary<GetRelationshipByIdQuery>, GetRelationshipByIdBoundary>();
            services.AddTransient<IValidator<GetRelationshipByIdQuery>, GetRelationshipByIdValidator>();

            // DELETE RELATIONSHIP
            services.AddTransient<IInteractor<DeleteRelationshipCommand>, DeleteRelationshipInteractor>();
            services.AddTransient<IBoundary<DeleteRelationshipCommand>, DeleteRelationshipBoundary>();
            services.AddTransient<IValidator<DeleteRelationshipCommand>, DeleteRelationshipValidator>();

            // UPDATE RELATIONSHIP
            services.AddTransient<IInteractor<UpdateRelationshipCommand>, UpdateRelationshipInteractor>();
            services.AddTransient<IBoundary<UpdateRelationshipCommand>, UpdateRelationshipBoundary>();
            services.AddTransient<IValidator<UpdateRelationshipCommand>, UpdateRelationshipValidator>();

            // MAPPERS
            services.AddTransient<IMapper<CreateRelationshipCommand, Relationship>, CreateRelationshipCommandToRelationshipMapper>();
            services.AddTransient<IMapper<UpdateRelationshipCommand, Relationship>, UpdateRelationshipCommandToRelationshipMapper>();

            return services;
        }
    }
}
