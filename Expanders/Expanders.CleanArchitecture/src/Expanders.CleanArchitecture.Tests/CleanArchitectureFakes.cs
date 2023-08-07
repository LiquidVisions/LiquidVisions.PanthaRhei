using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests
{
    /// <summary>
    /// Mocks for the <seealso cref="CleanArchitectureExpander"/> Project.
    /// </summary>
    public class CleanArchitectureFakes : Fakes
    {
        /// <summary>
        /// Gets the mock representing the Infrastructure <seealso cref="Component"/>.
        /// </summary>
        public Mock<Component> InfrastructureComponent { get; } = new ();

        /// <summary>
        /// Gets the mock  representing the Domain <seealso cref="Component"/>.
        /// </summary>
        public Mock<Component> DomainComponent { get; } = new ();

        /// <summary>
        /// Gets the mock  representing the Api <seealso cref="Component"/>.
        /// </summary>
        public Mock<Component> ApiComponent { get; } = new ();

        /// <summary>
        /// Gets the mock  representing the Application <seealso cref="Component"/>.
        /// </summary>
        public Mock<Component> ApplicationComponent { get; } = new ();

        /// <summary>
        /// Gets the mock representing the <seealso cref="Expander">Clean Arhcitectrure model</seealso>.
        /// </summary>

        public Mock<Expander> CleanArchitectureExpanderModel { get; } = new ();

        /// <summary>
        /// Gets the mock representing for <seealso cref="CleanArchitectureExpander"/>.
        /// </summary>
        public Mock<CleanArchitectureExpander> CleanArchitectureExpander { get; } = new ();

        /// <summary>
        /// Gets the mock representing for <seealso cref="IHarvestSerializer"/>.
        /// </summary>
        public Mock<IHarvestSerializer> IHarvestSerializerInteractor { get; } = new ();

        /// <summary>
        /// Gets a static value for representing the component output folder with the value of "C:\\Some\\Component\\Output\\Path".
        /// </summary>
        public string ExpectedCompontentOutputFolder { get; } = "C:\\Some\\Component\\Output\\Path";

        /// <summary>
        /// Gets a mocked <seealso cref="Entity"/> having a the name 'JustATestEntity'.
        /// </summary>
        public Entity ExpectedEntity { get; } = new () { Name = "JustATestEntity" };

        /// <summary>
        /// Gets a default app name.
        /// </summary>
        internal static string DefaultAppName { get; } = "Project";

        /// <summary>
        /// Gets a default namespace.
        /// </summary>
        internal static string DefaultAppFullName { get; } = "LiquidVisions.Tests";

        /// <summary>
        /// Gets a mock of <seealso cref="IProjectTemplate"/>.
        /// </summary>
        internal Mock<IProjectTemplate> IProjectTemplateInteractor { get; } = new ();

        /// <inheritdoc/>
        public override void ConfigureIDependencyFactory()
        {
            base.ConfigureIDependencyFactory();

            IDependencyFactory.Setup(x => x.Get<IProjectTemplate>())
                .Returns(IProjectTemplateInteractor.Object);

            IDependencyFactory.Setup(x => x.Get<IHarvestSerializer>())
                .Returns(IHarvestSerializerInteractor.Object);
        }

        /// <summary>
        /// Prepares the <seealso cref="CleanArchitectureExpander"/>.
        /// </summary>
        /// <param name="entities">A list of <seealso cref="Entity">Entities</seealso>.</param>
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
            IDependencyFactory.Setup(x => x.Get<CleanArchitectureExpander>()).Returns(CleanArchitectureExpander.Object);

            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.Api)).Returns(ApiComponent.Object);
            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.EntityFramework)).Returns(InfrastructureComponent.Object);
            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.Domain)).Returns(DomainComponent.Object);
            CleanArchitectureExpander.Setup(x => x.GetComponentByName(Resources.Application)).Returns(ApplicationComponent.Object);

            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(ApiComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(InfrastructureComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(DomainComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
            CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(ApplicationComponent.Object)).Returns(this.ExpectedCompontentOutputFolder);
        }

        /// <summary>
        /// Prepares the <seealso cref="App"/> model to be used for tests.
        /// </summary>
        /// <param name="expanders">A list of <seealso cref="Expander">Expanders.</seealso>.</param>
        /// <returns><seealso cref="App"/></returns>
        internal App SetupApp(List<Expander> expanders = null)
        {
            return SetupApp(GetValidEntities(), expanders);
        }

        /// <summary>
        /// Prepares the <seealso cref="App"/> model to be used for tests.
        /// </summary>
        /// <param name="entities">A list of <seealso cref="Entity">Entities</seealso>.</param>
        /// <param name="expanders">A list of <seealso cref="Expander">Expanders.</seealso>.</param>
        /// <returns><seealso cref="App"/></returns>
        internal App SetupApp(List<Entity> entities, List<Expander> expanders = null)
        {
            App app = GetDefaultApp(entities, expanders);

            IDependencyFactory.Setup(x => x.Get<App>()).Returns(app);

            return app;
        }

        /// <summary>
        /// Gets a list of valid entities.
        /// </summary>
        /// <returns><seealso cref="List{Entity}"/>.</returns>
        internal List<Entity> GetValidEntities()
        {
            List<Entity> entities = new ()
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
