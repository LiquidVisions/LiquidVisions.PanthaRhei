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
    public class AddEndpointsTests : AbstractCleanArchitectureTests
    {
        private readonly AddEndpoints handler;

        public AddEndpointsTests()
        {
            Fakes.MockAppWithMockedExpanders();
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
        public void Test()
        {
            // arrange
            string componentPath = "C:\\Some\\Folder\\Path";
            string endpointPath = Path.Combine(componentPath, Resources.EndpointFolder);
            string expextedFullPathToTemplate = Path.Combine(Extensions.GetTemplateFolder(Fakes.CleanArchitectureExpander.Object.Model, Fakes.Parameters.Object, Resources.EndpointTemplate));
            Fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(Fakes.ApiComponent.Object)).Returns(componentPath);

            // act
            handler.Execute();

            // assert
            Fakes.IDirectory.Verify(x => x.Create(endpointPath), Times.Once);
            Fakes.ITemplateInteractor.Verify(x => x.Render(expextedFullPathToTemplate, It.IsAny<object>()), Times.Exactly(4));
        }
    }
}
