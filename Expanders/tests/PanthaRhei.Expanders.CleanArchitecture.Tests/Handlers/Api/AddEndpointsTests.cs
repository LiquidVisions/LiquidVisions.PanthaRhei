using System.Collections.Generic;
using System.IO;
using System.Net;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Api
{
    public class AddEndpointsTests : AbstractCleanArchitectureTests
    {
        private readonly AddEndpoints handler;
        private readonly Entity expectedEntity = new();

        public AddEndpointsTests()
        {
            expectedEntity.Name = "JustATestEntity";

            Fakes.MockCleanArchitectureExpander(new List<Entity> { expectedEntity });
            handler = new(Fakes.CleanArchitectureExpanderInteractor.Object, Fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(8));
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
            string expextedFullPathToTemplate = Path.Combine(Extensions.GetTemplateFolder(Fakes.CleanArchitectureExpander.Object.Model, Fakes.Parameters.Object, Resources.EndpointTemplate));
            Fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(Fakes.ApiComponent.Object)).Returns(componentPath);

            var expectedTemplateParameters = new
            {
                applicationComponent = Fakes.ApplicationComponent.Object,
                clientComponent = Fakes.ClientComponent.Object,
                component = Fakes.ApiComponent.Object,
                Entity = expectedEntity,
            };

            string expectedPathToWrite = Path.Combine(endpointPath, $"{expectedEntity.Name}{Resources.EndpointFolder}.cs");
            string expectedRenderedTemplate = "RenderedResult";
            Fakes.ITemplateInteractor.Setup(x => x.Render(expextedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == expectedTemplateParameters.GetHashCode()))).Returns(expectedRenderedTemplate);

            // act
            handler.Execute();

            // assert
            Fakes.IDirectory.Verify(x => x.Create(endpointPath), Times.Once);
            Fakes.ITemplateInteractor.Verify(x => x.Render(expextedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == expectedTemplateParameters.GetHashCode())), Times.Once);
            Fakes.IFile.Verify(x => x.WriteAllText(expectedPathToWrite, expectedRenderedTemplate), Times.Once);
        }

        [Fact]
        public void Execute_ShouldModifyBootstrapperFile()
        {
            // arrange
            string componentPath = "C:\\Some\\Folder\\Path";
            string expectedPathToBootstrapperFile = Path.Combine(componentPath, Resources.DependencyInjectionBootstrapperFile);
            Fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(Fakes.ApiComponent.Object)).Returns(componentPath);
            Fakes.IWriterInteractor.Setup(x => x.IndexOf("return services;")).Returns(5);
            Fakes.IWriterInteractor.Setup(x => x.IndexOf("app.Run();")).Returns(12);


            // act
            handler.Execute();

            // assert
            Fakes.IWriterInteractor.Verify(x => x.Load(expectedPathToBootstrapperFile), Times.Once);
            Fakes.IWriterInteractor.Verify(x => x.IndexOf("return services;"), Times.Once);
            Fakes.IWriterInteractor.Verify(x => x.WriteAt(4, string.Empty), Times.Once);
            Fakes.IWriterInteractor.Verify(x => x.WriteAt(5, $"            services.Add{expectedEntity.Name}Elements();"), Times.Once);

            Fakes.IWriterInteractor.Verify(x => x.IndexOf("app.Run();"), Times.Once);
            Fakes.IWriterInteractor.Verify(x => x.WriteAt(11, string.Empty), Times.Once);
            Fakes.IWriterInteractor.Verify(x => x.WriteAt(12, $"            app.Use{expectedEntity.Name}Endpoints();"), Times.Once);

            Fakes.IWriterInteractor.Verify(x => x.Save(expectedPathToBootstrapperFile), Times.Once);
        }
    }
}
