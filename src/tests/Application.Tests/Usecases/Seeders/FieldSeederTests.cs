using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Castle.Components.DictionaryAdapter;
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
        private readonly Fakes _fakes = new();
        private readonly FieldSeeder _interactor;
        private readonly Mock<IDeleteRepository<Field>> _mockedDeleteGateway = new();
        private readonly Mock<ICreateRepository<Field>> _mockedCreateGateway = new();
        private readonly Mock<IEntitiesToSeedRepository> _mockedEntityToSeedGateway = new();
        private readonly Mock<IModelConfiguration> _mockedModelConfiguration = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSeederTests"/> class.
        /// </summary>
        public FieldSeederTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Field>>()).Returns(_mockedCreateGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Field>>()).Returns(_mockedDeleteGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IEntitiesToSeedRepository>()).Returns(_mockedEntityToSeedGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IModelConfiguration>()).Returns(_mockedModelConfiguration.Object);

            _interactor = new FieldSeeder(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Field>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Field>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IEntitiesToSeedRepository>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IModelConfiguration>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(4));
            _fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
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
            Assert.Equal(6, _interactor.SeedOrder);
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
            Assert.Equal(6, _interactor.ResetOrder);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            _interactor.Reset();

            // assert
            _mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/>.
        /// </summary>
        [Theory]
        [InlineData(typeof(publicClassWithIntField), nameof(publicClassWithIntField.IntField), "public", "int", false, null, null, "virtual")]
        [InlineData(typeof(abstractClassWithStringField), nameof(abstractClassWithStringField.StringField), "public", "string", false, null, null, "abstract")]
        [InlineData(typeof(publicClassWithBooleanField), nameof(publicClassWithBooleanField.BooleanField), "public", "bool", false, null, null, null)]
        [InlineData(typeof(publicClassWithDecimalField), nameof(publicClassWithDecimalField.DecimalField), "public", "decimal", false, null, null, null)]
        [InlineData(typeof(publicClassWithStringField), nameof(publicClassWithStringField.StringField), "public", "string", false, null, new string[] { nameof(publicClassWithStringField.StringField) }, null)]
        [InlineData(typeof(publicClassWithGuidField), nameof(publicClassWithGuidField.GuidField), "public", "Guid", false, new string[] { nameof(publicClassWithGuidField.GuidField) }, null, null)]
        [InlineData(typeof(publicClassWithCollectionField), nameof(publicClassWithCollectionField.CollectionField), "public", "string", true, null, null, null)]
        public void SeedShouldCreate(Type type, string expectedName, string expectedModifier, string expectedReturnType, bool isCollection, string[] keys = null, string[] indexes = null, string behaviour = null)
        {
            ArgumentNullException.ThrowIfNull(type, nameof(type));

            // arrange
            App app = Arrange(type, keys, indexes, null);

            // act
            _interactor.Seed(app);

            // assert
            Assert.Single(app.Entities);
            Assert.Single(app.Entities.Single().Fields);
            Field field = app.Entities.Single().Fields.Single();
            Assert.Equal(expectedName, field.Name);
            Assert.Equal(1, field.Order);
            Assert.Equal(expectedReturnType, field.ReturnType);
            Assert.Equal(isCollection, field.IsCollection);
            Assert.Equal(field.IsKey, keys != null);
            Assert.Equal(field.IsIndex, indexes != null);
            Assert.Null(field.Size);
            Assert.False(field.Required);
            Assert.Equal(expectedModifier, field.Modifier);
            
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Field>(x => x == field)), Times.Once);
        }

        private App Arrange(Type type, string[] keys = null, string[] indexes = null, int? size = null)
        {
            Entity entity = new() { Name = type.Name };
            App app = new();
            app.Entities.Add(entity);

            if(keys != null)
            {
                _mockedModelConfiguration
                    .Setup(x => x.GetKeys(type))
                    .Returns(keys);
            }

            if(indexes != null)
            {
                _mockedModelConfiguration
                    .Setup(x => x.GetIndexes(type))
                    .Returns(indexes);
            }

            if(size != null)
            {
                _mockedModelConfiguration
                    .Setup(x => x.GetSize(type, It.IsAny<string>()))
                    .Returns(size);
            }

            _mockedEntityToSeedGateway
                .Setup(x => x.GetAll())
                .Returns([type]);

            return app;
        }

        public class publicClassWithIntField
        {
            public virtual int IntField  { get; set; }
        }

        public class publicClassWithStringField
        {
            public string StringField { get; set; }
        }

        public abstract class abstractClassWithStringField
        {
            public abstract string StringField { get; set; }
        }

        public class publicClassWithBooleanField
        {
            public bool BooleanField { get; set; }
        }

        public class publicClassWithDecimalField
        {
            public decimal DecimalField { get; set; }
        }

        public class publicClassWithGuidField
        {
            public Guid GuidField { get; set; }
        }

        public class publicClassWithCollectionField
        {
            public ICollection<string> CollectionField { get; set; }
        }

        public class publicClassWithPrivateField
        {
            private string PrivateField { get; set; }
        }
    }
}
