using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
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
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(8));
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
            fakes.ITemplateInteractor.Verify(x => x.Render(expextedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == expectedTemplateParameters.GetHashCode())), Times.Once);
            fakes.IFile.Verify(x => x.WriteAllText(expectedPathToWrite, expectedRenderedTemplate), Times.Once);
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
