using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    public class ExpandEndpointsHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly ExpandEndpointsHandlerInteractor handler;

        public ExpandEndpointsHandlerInteractorTests()
        {
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(16, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandEndpointsHandlerInteractor), handler.Name);
        }

        [Theory]
        [InlineData(GenerationModes.Default, true)]
        [InlineData(GenerationModes.Migrate, false)]
        [InlineData(GenerationModes.Extend, true)]
        [InlineData(GenerationModes.Deploy, false)]
        [InlineData(GenerationModes.None, false)]
        public void CanExecute_ShouldBeFalse(GenerationModes mode, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        [Fact]
        public void Execute_ShouldRenderAndSaveTemplate()
        {
            // arrange
            string componentPath = fakes.ExpectedCompontentOutputFolder;
            string endpointPath = Path.Combine(componentPath, CleanArchitectureResources.EndpointFolder);
            string expextedFullPathToTemplate = Path.Combine(Extensions.GetPathToTemplate(fakes.CleanArchitectureExpander.Object.Model, fakes.GenerationOptions.Object, CleanArchitectureResources.EndpointTemplate));

            var expectedTemplateParameters = new
            {
                applicationComponent = fakes.ApplicationComponent.Object,
                component = fakes.ApiComponent.Object,
                Entity = fakes.ExpectedEntity,
            };

            string expectedPathToWrite = Path.Combine(endpointPath, $"{fakes.ExpectedEntity.Name}{CleanArchitectureResources.EndpointFolder}.cs");
            string expectedRenderedTemplate = "RenderedResult";
            fakes.ITemplateInteractor.Setup(x => x.Render(expextedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == expectedTemplateParameters.GetHashCode()))).Returns(expectedRenderedTemplate);

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(endpointPath), Times.Once);
            fakes.ITemplateInteractor.Verify(x => x.RenderAndSave(expextedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == expectedTemplateParameters.GetHashCode()), expectedPathToWrite), Times.Once);
        }

        [Fact]
        public void Execute_ShouldModifyBootstrapperFile()
        {
            // arrange
            string componentPath = fakes.ExpectedCompontentOutputFolder;
            string expectedPathToBootstrapperFile = Path.Combine(componentPath, CleanArchitectureResources.DependencyInjectionBootstrapperFile);
            fakes.IWriterInteractor.Setup(x => x.IndexOf("return services;")).Returns(5);
            fakes.IWriterInteractor.Setup(x => x.IndexOf("app.Run();")).Returns(12);

            // act
            handler.Execute();

            // assert
            fakes.IWriterInteractor.Verify(x => x.Load(expectedPathToBootstrapperFile), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.IndexOf("return services;"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(4, string.Empty), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(5, $"            services.Add{fakes.ExpectedEntity.Name}Elements();"), Times.Once);

            fakes.IWriterInteractor.Verify(x => x.IndexOf("app.Run();"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(11, string.Empty), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(12, $"            app.Use{fakes.ExpectedEntity.Name}Endpoints();"), Times.Once);

            fakes.IWriterInteractor.Verify(x => x.Save(expectedPathToBootstrapperFile), Times.Once);
        }
    }
}
