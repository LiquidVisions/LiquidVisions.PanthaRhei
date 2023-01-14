﻿using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests
{
    public class CleanArchitectureFakes : Fakes
    {
        public CleanArchitectureFakes()
        {
            IDependencyFactoryInteractor.Setup(x => x.Get<IProjectAgentInteractor>())
                .Returns(IProjectAgentInteractor.Object);

        }

        public Mock<CleanArchitectureExpander> CleanArchitectureExpanderInteractor { get; } = new();

        public Mock<Component> InfrastructureComponent { get; } = new();

        public Mock<Component> DomainComponent { get; } = new();

        public Mock<Component> ApiComponent { get; } = new();

        public Mock<Component> ApplicationComponent { get; } = new();

        public Mock<Component> ClientComponent { get; } = new();

        public Mock<Expander> CleanArchitectureExpanderModel { get; } = new();

        public Mock<CleanArchitectureExpander> CleanArchitectureExpander { get; } = new();

        public Mock<IProjectAgentInteractor> IProjectAgentInteractor { get; } = new();

        internal App MockAppWithMockedExpanders()
        {
            CleanArchitectureExpanderInteractor.Setup(x => x.Name).Returns(nameof(CleanArchitectureExpanderInteractor));
            CleanArchitectureExpanderInteractor.Setup(x => x.Model).Returns(CleanArchitectureExpanderModel.Object);

            App app = GetDefaultApplication(new List<Expander> { CleanArchitectureExpanderModel.Object });
            CleanArchitectureExpanderModel.Setup(x => x.TemplateFolder).Returns(".Templates");
            CleanArchitectureExpanderModel.Setup(x => x.Name).Returns("CleanArchitectgure");

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

            IDependencyFactoryInteractor.Setup(x => x.Get<App>()).Returns(app);
            CleanArchitectureExpander.Setup(x => x.Model).Returns(CleanArchitectureExpanderModel.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<CleanArchitectureExpander>()).Returns(CleanArchitectureExpander.Object);

            return app;

        }

        public App GetDefaultApplication(List<Expander> expanders = null)
        {
            return GetApplication(GetValidEntities(), expanders);
        }

        private App GetApplication(List<Entity> entities, List<Expander> expanders = null)
        {
            return new App
            {
                FullName = "LiquidVisions.Tests",
                Name = "Project",
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

        private App GetDefaultApplication(List<Entity> entities, List<Expander> expanders = null)
        {
            return new App
            {
                FullName = "LiquidVisions.Tests",
                Name = "Project",
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