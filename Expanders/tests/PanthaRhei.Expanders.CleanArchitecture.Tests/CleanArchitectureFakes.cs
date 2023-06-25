using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests
{
    public class CleanArchitectureFakes : Fakes
    {
        public Mock<Component> InfrastructureComponent { get; } = new();

        public Mock<Component> DomainComponent { get; } = new();

        public Mock<Component> ApiComponent { get; } = new();

        public Mock<Component> ApplicationComponent { get; } = new();

        public Mock<Expander> CleanArchitectureExpanderModel { get; } = new();

        public Mock<CleanArchitectureExpander> CleanArchitectureExpander { get; } = new();

        public Mock<IHarvestSerializerInteractor> IHarvestSerializerInteractor { get; } = new();

        public string ExpectedCompontentOutputFolder { get; } = "C:\\Some\\Component\\Output\\Path";

        public Entity ExpectedEntity { get; } = new() { Name = "JustATestEntity" };

        internal static string DefaultAppName { get; } = "Project";

        internal static string DefaultAppFullName { get; } = "LiquidVisions.Tests";

        internal Mock<IProjectTemplateInteractor> IProjectTemplateInteractor { get; } = new();

        public override void ConfigureIDependencyFactoryInteractor()
        {
            base.ConfigureIDependencyFactoryInteractor();

            IDependencyFactoryInteractor.Setup(x => x.Get<IProjectTemplateInteractor>())
                .Returns(IProjectTemplateInteractor.Object);

            IDependencyFactoryInteractor.Setup(x => x.Get<IHarvestSerializerInteractor>())
                .Returns(IHarvestSerializerInteractor.Object);
        }

        internal void MockCleanArchitectureExpander(List<Entity> entities = null)
        {
            SetupApp(entities, new List<Expander> { CleanArchitectureExpanderModel.Object });

            CleanArchitectureExpander.Setup(x => x.Name).Returns(nameof(CleanArchitectureExpander));
            CleanArchitectureExpander.Setup(x => x.Model).Returns(CleanArchitectureExpanderModel.Object);

            CleanArchitectureExpanderModel.Setup(x => x.TemplateFolder).Returns(".Templates");
            CleanArchitectureExpanderModel.Setup(x => x.Name).Returns("CleanArchitecture");
            CleanArchitectureExpanderModel.Setup(x => x.Order).Returns(2);

            InfrastructureComponent.Setup(x => x.Name).Returns(Resources.EntityFramework);
            DomainComponent.Setup(x => x.Name).Returns(Resources.Domain);
            ApiComponent.Setup(x => x.Name).Returns(Resources.Api);
            ApplicationComponent.Setup(x => x.Name).Returns(Resources.Application);

            CleanArchitectureExpanderModel.Setup(x => x.Components).Returns(
                new List<Component>
                {
                    InfrastructureComponent.Object,
                    DomainComponent.Object,
                    ApiComponent.Object,
                    ApplicationComponent.Object,
                });

            CleanArchitectureExpander.Setup(x => x.Model).Returns(CleanArchitectureExpanderModel.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<CleanArchitectureExpander>()).Returns(CleanArchitectureExpander.Object);

            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.Api)).Returns(ApiComponent.Object);
            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.EntityFramework)).Returns(InfrastructureComponent.Object);
            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.Domain)).Returns(DomainComponent.Object);
            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.Application)).Returns(ApplicationComponent.Object);

            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(ApiComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(InfrastructureComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(DomainComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(ApplicationComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
        }

        internal App SetupApp(List<Expander> expanders = null)
        {
            return SetupApp(GetValidEntities(), expanders);
        }

        internal App SetupApp(List<Entity> entities, List<Expander> expanders = null)
        {
            App app = GetDefaultApp(entities, expanders);

            IDependencyFactoryInteractor.Setup(x => x.Get<App>()).Returns(app);

            return app;
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "No static methods")]
        internal List<Entity> GetValidEntities()
        {
            List<Entity> entities = new()
            {
                new Entity
                {
                    Name = "EntityWithSingleKey",
                    Fields = new List<Field>
                    {
                        new Field { Name = "Key", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field { Name = "Field1", ReturnType = "string", Required = true },
                    },
                },
                new Entity
                {
                    Name = "EntityWithClusteredKey",
                    Fields = new List<Field>
                    {
                        new Field { Name = "Key1", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field { Name = "Key2", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field { Name = "Field1", ReturnType = "Guid", Required = true },
                    },
                },
                new Entity
                {
                    Name = "EntityWithSingleIndex",
                    Fields = new List<Field>
                    {
                        new Field { Name = "Key1", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field { Name = "Index", IsIndex = true, ReturnType = "string", Required = true },
                        new Field { Name = "Field1", ReturnType = "string", Required = true },
                    },
                },
                new Entity
                {
                    Name = "EntityWithClusteredIndex",
                    Fields = new List<Field>
                    {
                        new Field { Name = "Key", IsKey = true, ReturnType = "string", Required = true },
                        new Field { Name = "Index1", IsIndex = true, ReturnType = "string", Required = true },
                        new Field { Name = "Index2", IsIndex = true, ReturnType = "string", Required = true },
                        new Field { Name = "Field1", ReturnType = "string", Required = true },
                    },
                },
            };

            foreach (Entity entity in entities)
            {
                foreach (Field field in entity.Fields)
                {
                    field.Entity = entity;
                }
            }

            return entities;
        }

        private static App GetDefaultApp(List<Entity> entities, List<Expander> expanders = null)
        {
            return new App
            {
                FullName = DefaultAppFullName,
                Name = DefaultAppName,
                Expanders = expanders ?? new List<Expander>(),
                ConnectionStrings = new List<ConnectionString>
                {
                    new ConnectionString
                    {
                        Name = "DefaultConnectionString",
                        Definition = "SomeConnectionStringDefinition",
                    },
                },
                Entities = entities,
            };
        }
    }
}
