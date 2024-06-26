﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Castle.Components.DictionaryAdapter;
using LiquidVisions.PanthaRhei.Application.Tests.Mocks;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <see cref="FieldSeeder"/>.
    /// </summary>
    public class FieldSeederTests
    {
        private readonly Fakes fakes = new();
        private readonly FieldSeeder interactor;
        private readonly Mock<IDeleteRepository<Field>> mockedDeleteGateway = new();
        private readonly Mock<ICreateRepository<Field>> mockedCreateGateway = new();
        private readonly Mock<IEntitiesToSeedRepository> mockedEntityToSeedGateway = new();
        private readonly Mock<IModelConfiguration> mockedModelConfiguration = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSeederTests"/> class.
        /// </summary>
        public FieldSeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Field>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Field>>()).Returns(mockedDeleteGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IEntitiesToSeedRepository>()).Returns(mockedEntityToSeedGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IModelConfiguration>()).Returns(mockedModelConfiguration.Object);

            interactor = new FieldSeeder(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Field>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Field>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IEntitiesToSeedRepository>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IModelConfiguration>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(4));
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe6()
        {
            // arrange
            // act
            // assert
            Assert.Equal(6, interactor.SeedOrder);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe6()
        {
            // arrange
            // act
            // assert
            Assert.Equal(6, interactor.ResetOrder);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            interactor.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/>.
        /// </summary>
        [Theory]
        [InlineData(typeof(PublicClassWithIntField), nameof(PublicClassWithIntField.IntField), "public", "int", false, null, null, "virtual")]
        [InlineData(typeof(AbstractClassWithStringField), nameof(AbstractClassWithStringField.StringField), "public", "string", false, null, null, "abstract")]
        [InlineData(typeof(PublicClassWithBooleanField), nameof(PublicClassWithBooleanField.BooleanField), "public", "bool", false, null, null, null)]
        [InlineData(typeof(PublicClassWithDecimalField), nameof(PublicClassWithDecimalField.DecimalField), "public", "decimal", false, null, null, null)]
        [InlineData(typeof(PublicClassWithStringField), nameof(PublicClassWithStringField.StringField), "public", "string", false, null, new string[] { nameof(PublicClassWithStringField.StringField) }, null)]
        [InlineData(typeof(PublicClassWithGuidField), nameof(PublicClassWithGuidField.GuidField), "public", "Guid", false, new string[] { nameof(PublicClassWithGuidField.GuidField) }, null, null)]
        [InlineData(typeof(PublicClassWithCollectionField), nameof(PublicClassWithCollectionField.CollectionField), "public", "string", true, null, null, null)]
        [InlineData(typeof(ClassWithComplexField), nameof(ClassWithComplexField.ComplexField), "public", nameof(ComplexField), false, null, null, null, nameof(ComplexField))]
        public void SeedShouldCreate(Type type, string expectedName, string expectedModifier, string expectedReturnType, bool isCollection, string[] keys = null, string[] indexes = null, string behaviour = null, string complexFieldName = null)
        {
            ArgumentNullException.ThrowIfNull(type, nameof(type));

            // arrange
            App app = Arrange(type, keys, indexes, null, complexFieldName);

            // act
            interactor.Seed(app);

            // assert
            Assert.Single(app.Entities.Single(x => x.Name == type.Name).Fields);
            Field field = app.Entities.Single(x => x.Name == type.Name).Fields.Single();
            Assert.Equal(expectedName, field.Name);
            Assert.Equal(1, field.Order);
            Assert.Equal(expectedReturnType, field.ReturnType);
            Assert.Equal(isCollection, field.IsCollection);
            Assert.Equal(field.IsKey, keys != null);
            Assert.Equal(field.IsIndex, indexes != null);
            Assert.Null(field.Size);
            Assert.False(field.Required);
            Assert.Equal(behaviour, field.Behaviour);
            Assert.Equal(expectedModifier, field.Modifier);

            mockedCreateGateway.Verify(x => x.Create(It.Is<Field>(x => x == field)), Times.Once);
        }

        private App Arrange(Type type, string[] keys = null, string[] indexes = null, int? size = null, string complexFieldName = null)
        {
            App app = new();

            Entity entity = new() { Name = type.Name };
            app.Entities.Add(entity);
            if(!string.IsNullOrEmpty(complexFieldName))
            {
                app.Entities.Add(new Entity() { Name = complexFieldName });
            }

            if (keys != null)
            {
                mockedModelConfiguration
                    .Setup(x => x.GetKeys(type))
                    .Returns(keys);
            }

            if (indexes != null)
            {
                mockedModelConfiguration
                    .Setup(x => x.GetIndexes(type))
                    .Returns(indexes);
            }

            if (size != null)
            {
                mockedModelConfiguration
                    .Setup(x => x.GetSize(type, It.IsAny<string>()))
                    .Returns(size);
            }

            mockedEntityToSeedGateway
                .Setup(x => x.GetAll())
                .Returns([type]);

            return app;
        }
    }
}
