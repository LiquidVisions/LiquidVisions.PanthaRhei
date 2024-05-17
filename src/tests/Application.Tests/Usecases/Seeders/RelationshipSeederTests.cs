using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Models;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for the <see cref="RelationshipSeeder"/> class.
    /// </summary>
    public class RelationshipSeederTests
    {
        private readonly ApplicationFakes fakes = new();
        private readonly RelationshipSeeder seeder;
        private readonly Mock<IDeleteRepository<Relationship>> mockedDeleteGateway = new();
        private readonly Mock<ICreateRepository<Relationship>> mockedCreateGateway = new();
        private readonly Mock<IModelConfiguration> mockedModelConfiguration = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="RelationshipSeederTests"/> class.
        /// </summary>
        public RelationshipSeederTests()
        {
            mockedCreateGateway = new();
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Relationship>>()).Returns(mockedCreateGateway.Object);

            mockedDeleteGateway = new();
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Relationship>>()).Returns(mockedDeleteGateway.Object);

            mockedModelConfiguration = new();
            fakes.IDependencyFactory.Setup(x => x.Resolve<IModelConfiguration>()).Returns(mockedModelConfiguration.Object);

            seeder = new(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Relationship>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Relationship>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IModelConfiguration>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(3));
        }

        /// <summary>
        /// Test for <see cref="RelationshipSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe7()
        {
            // arrange
            // act
            // assert
            Assert.Equal(7, seeder.SeedOrder);
        }

        /// <summary>
        /// Test for <see cref="RelationshipSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe0()
        {
            // arrange
            // act
            // assert
            Assert.Equal(0, seeder.ResetOrder);
        }

        /// <summary>
        /// Test for <see cref="RelationshipSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            seeder.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="RelationshipSeeder.Seed"/>.
        /// </summary>
        [Fact]
        public void SeedRelationshipShouldBeCreated()
        {
            // arrange
            App app = MockRelationshipDto();

            // act
            seeder.Seed(app);

            // assert
            mockedModelConfiguration.Verify(x => x.GetRelationshipInfo(It.IsAny<Entity>()), Times.Exactly(app.Entities.Count));
            mockedCreateGateway.Verify(x => x.Create(It.Is<Relationship>(rel =>
                rel.Required == true
                && rel.Key.Name == "Id"
                && rel.Key.RelationshipKeys.Single().Entity == app.Entities.First()
                && rel.Cardinality == "WithOne"
                && rel.WithForeignEntity.Name == app.Entities.Last().Name
                && rel.WithForeignEntity.IsForeignEntityOf.Single().Entity == app.Entities.First()
                && rel.WithForeignEntityKey.Name == app.Entities.Last().Fields.First().Name
                && rel.WithForeignEntityKey.IsForeignEntityKeyOf.Single().Entity == app.Entities.First()
                && rel.WithCardinality == "WithOne"
                && rel.WithForeignEntityKey.IsForeignEntityKeyOf.Single().Entity == app.Entities.First()
            )), Times.Once);
        }

        private App MockRelationshipDto()
        {
            App app = new();
            Entity entity1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Entity1",
                App = app,
                Fields =
                [
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Id",
                        IsKey = true,
                        Required = true,
                    },
                ],
            };

            app.Entities.Add(entity1);

            Entity entity2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Entity2",
                App = app,
                Fields =
                [
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Entity1Key",
                        IsKey = true,
                        Required = true,
                    },
                ],
            };
            app.Entities.Add(entity2);

            mockedModelConfiguration.Setup(x => x.GetRelationshipInfo(entity2)).Returns([]);
            mockedModelConfiguration.Setup(x => x.GetRelationshipInfo(entity1)).Returns(
            [
                new() {
                    Key = "Id",
                    Entity = "Entity1",
                    Cardinality = "WithOne",
                    WithForeignEntity = "Entity2",
                    WithForeignEntityKey = "Entity1Key",
                    WithyCardinality = "WithOne",
                    Required = true,
                },
            ]);

            return app;
        }
    }
}
