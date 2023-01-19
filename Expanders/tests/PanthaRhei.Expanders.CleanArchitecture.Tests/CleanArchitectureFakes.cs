﻿using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests
{
    public class CleanArchitectureFakes : Fakes
    {
        public Mock<CleanArchitectureExpander> CleanArchitectureExpanderInteractor { get; } = new();

        public Mock<Component> InfrastructureComponent { get; } = new();

        public Mock<Component> DomainComponent { get; } = new();

        public Mock<Component> ApiComponent { get; } = new();

        public Mock<Component> ApplicationComponent { get; } = new();

        public Mock<Component> ClientComponent { get; } = new();

        public Mock<Expander> CleanArchitectureExpanderModel { get; } = new();

        public Mock<CleanArchitectureExpander> CleanArchitectureExpander { get; } = new();

        internal Mock<IProjectTemplateInteractor> IProjectTemplateInteractor { get; } = new();

        public Mock<IProjectAgentInteractor> IProjectAgentInteractor { get; } = new();

        public string ExpectedCompontentOutputFolder = "C:\\Some\\Component\\Output\\Path";

        internal void MockCleanArchitectureExpander(List<Entity> entities = null)
        {
            App app = SetupApp(entities, new List<Expander> { CleanArchitectureExpanderModel.Object });

            CleanArchitectureExpanderInteractor.Setup(x => x.Name).Returns(nameof(CleanArchitectureExpanderInteractor));
            CleanArchitectureExpanderInteractor.Setup(x => x.Model).Returns(CleanArchitectureExpanderModel.Object);

            CleanArchitectureExpanderModel.Setup(x => x.TemplateFolder).Returns(".Templates");
            CleanArchitectureExpanderModel.Setup(x => x.Name).Returns("CleanArchitecture");

            InfrastructureComponent.Setup(x => x.Name).Returns(Resources.EntityFramework);
            InfrastructureComponent.Setup(x => x.Name).Returns(Resources.EntityFramework);
            DomainComponent.Setup(x => x.Name).Returns(Resources.Domain);
            ApiComponent.Setup(x => x.Name).Returns(Resources.Api);
            ApplicationComponent.Setup(x => x.Name).Returns(Resources.Application);
            ClientComponent.Setup(x => x.Name).Returns(Resources.Client);

            CleanArchitectureExpanderModel.Setup(x => x.Components).Returns(
                new List<Component>
                {
                    InfrastructureComponent.Object,
                    DomainComponent.Object,
                    ApiComponent.Object,
                    ApplicationComponent.Object,
                    ClientComponent.Object,
                });

            CleanArchitectureExpander.Setup(x => x.Model).Returns(CleanArchitectureExpanderModel.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<CleanArchitectureExpander>()).Returns(CleanArchitectureExpander.Object);
        }

        public override void ConfigureIDependencyFactoryInteractor()
        {
            base.ConfigureIDependencyFactoryInteractor();

            IDependencyFactoryInteractor.Setup(x => x.Get<IProjectTemplateInteractor>())
                .Returns(IProjectTemplateInteractor.Object);

            IDependencyFactoryInteractor.Setup(x => x.Get<IProjectAgentInteractor>())
                .Returns(IProjectAgentInteractor.Object);
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

        internal static string DefaultAppName => "Project";
        
        internal static string DefaultAppFullName => "LiquidVisions.Tests";

        private App GetDefaultApp(List<Entity> entities, List<Expander> expanders = null)
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

        private List<Entity> GetValidEntities()
        {
            List<Entity> entities = new()
            {
                new Entity
                {
                    Name = "EntityWithSingleKey",
                    Fields = new List<Field>
                    {
                        new Field{ Name = "Key", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field{ Name = "Field1", ReturnType = "string", Required = true },
                    },
                },
                new Entity
                {
                    Name = "EntityWithClusteredKey",
                    Fields = new List<Field>
                    {
                        new Field{ Name = "Key1", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field{ Name = "Key2", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field{ Name = "Field1", ReturnType = "Guid", Required = true },
                    },
                },
                new Entity
                {
                    Name = "EntityWithSingleIndex",
                    Fields = new List<Field>
                    {
                        new Field{ Name = "Key1", IsKey = true, ReturnType = "Guid", Required = true },
                        new Field{ Name = "Index", IsIndex = true, ReturnType = "string", Required = true },
                        new Field{ Name = "Field1", ReturnType = "string", Required = true },
                    },
                },
                new Entity
                {
                    Name = "EntityWithClusteredIndex",
                    Fields = new List<Field>
                    {
                        new Field{ Name = "Key", IsKey = true, ReturnType = "string", Required = true},
                        new Field{ Name = "Index1", IsIndex = true, ReturnType = "string", Required = true },
                        new Field{ Name = "Index2", IsIndex = true, ReturnType = "string", Required = true },
                        new Field{ Name = "Field1", ReturnType = "string", Required = true },
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
    }
}
