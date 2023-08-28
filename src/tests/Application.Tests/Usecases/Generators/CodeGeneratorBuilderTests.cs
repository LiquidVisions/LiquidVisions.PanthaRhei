using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Generators
{
    /// <summary>
    /// Tests for <see cref="CodeGeneratorBuilder"/>.
    /// </summary>
    public class CodeGeneratorBuilderTests
    {
        private readonly CodeGeneratorBuilder _interactor;
        private readonly Mock<IGetRepository<App>> _mockedGetGateway = new();
        private readonly ApplicationFakes _fakes = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilderTests"/> class.
        /// </summary>
        public CodeGeneratorBuilderTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Get<IGetRepository<App>>()).Returns(_mockedGetGateway.Object);

            _interactor = new CodeGeneratorBuilder(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests the <see cref="CodeGeneratorBuilder.Build"/> method while throwing an <seealso cref="CodeGenerationException"/>.
        /// </summary>
        [Fact]
        public void Build_ParametersHasNoValueAppId_ShouldThrowException()
        {
            // arrange
            Guid id = Guid.NewGuid();
            _fakes.GenerationOptions.Setup(x => x.AppId).Returns(id);
            _mockedGetGateway.Setup(x => x.GetById(id)).Returns((App)null);

            // act
            void Action() => _interactor.Build();

            // assert
            CodeGenerationException exception = Assert.Throws<CodeGenerationException>(Action);
            Assert.Equal($"No application model available with the provided Id {id}.", exception.Message);
        }

        /// <summary>
        /// Tests the <see cref="CodeGeneratorBuilder.Build"/> method.
        /// </summary>
        [Fact]
        public void Build_HappyFlow_ShouldVerify()
        {
            // arrange
            Guid id = Guid.NewGuid();
            _fakes.GenerationOptions.Setup(x => x.AppId).Returns(id);
            App app = new();
            _mockedGetGateway.Setup(x => x.GetById(id)).Returns(app);
            _fakes.IDependencyManager.Setup(x => x.Build()).Returns(_fakes.IDependencyFactory.Object);

            // act
            _interactor.Build();

            // assert
            _fakes.IDependencyManager.Verify(x => x.AddSingleton(app), Times.Once);
            _fakes.IExpanderPluginLoader.Verify(x => x.LoadAllRegisteredPluginsAndBootstrap(app), Times.Once);
            _fakes.IDependencyManager.Verify(x => x.Build(), Times.Once);
        }
    }
}
