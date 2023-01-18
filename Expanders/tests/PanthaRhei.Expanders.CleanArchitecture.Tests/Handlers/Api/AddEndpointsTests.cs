using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Api
{
    public class AddEndpointsTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly AddEndpoints handler;
        private readonly Entity expectedEntity = new();

        public AddEndpointsTests()
        {
            expectedEntity.Name = "JustATestEntity";

            fakes.MockCleanArchitectureExpander(new List<Entity> { expectedEntity });
            handler = new(fakes.CleanArchitectureExpanderInteractor.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Domain.Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(6));
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
            Assert.Equal(nameof(AddEndpoints), handler.Name);
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
            fakes.Parameters.Setup(x => x.GenerationMode).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.Parameters.Setup(x => x.GenerationMode).Returns(GenerationModes.Default);
            fakes.Parameters.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Fact]
        public void Execute_ShouldRenderAndSaveTemplate()
        {
            // arrange
            string componentPath = "C:\\Some\\Folder\\Path";
            string endpointPath = Path.Combine(componentPath, Resources.EndpointFolder);
            string expextedFullPathToTemplate = Path.Combine(Extensions.GetTemplateFolder(fakes.CleanArchitectureExpander.Object.Model, fakes.Parameters.Object, Resources.EndpointTemplate));
            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.ApiComponent.Object)).Returns(componentPath);

            var expectedTemplateParameters = new
            {
                applicationComponent = fakes.ApplicationComponent.Object,
                clientComponent = fakes.ClientComponent.Object,
                component = fakes.ApiComponent.Object,
                Entity = expectedEntity,
            };

            string expectedPathToWrite = Path.Combine(endpointPath, $"{expectedEntity.Name}{Resources.EndpointFolder}.cs");
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
            string componentPath = "C:\\Some\\Folder\\Path";
            string expectedPathToBootstrapperFile = Path.Combine(componentPath, Resources.DependencyInjectionBootstrapperFile);
            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.ApiComponent.Object)).Returns(componentPath);
            fakes.IWriterInteractor.Setup(x => x.IndexOf("return services;")).Returns(5);
            fakes.IWriterInteractor.Setup(x => x.IndexOf("app.Run();")).Returns(12);


            // act
            handler.Execute();

            // assert
            fakes.IWriterInteractor.Verify(x => x.Load(expectedPathToBootstrapperFile), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.IndexOf("return services;"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(4, string.Empty), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(5, $"            services.Add{expectedEntity.Name}Elements();"), Times.Once);

            fakes.IWriterInteractor.Verify(x => x.IndexOf("app.Run();"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(11, string.Empty), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.WriteAt(12, $"            app.Use{expectedEntity.Name}Endpoints();"), Times.Once);

            fakes.IWriterInteractor.Verify(x => x.Save(expectedPathToBootstrapperFile), Times.Once);
        }
    }
}
