using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Application.RequestModels;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Seeders
{
    public class IExpanderSeederInteractorTests
    {
        private readonly ExpanderSeederInteractor interactor;
        private readonly Fakes fakes = new();
        private readonly Mock<ICreateGateway<Expander>> mockedCreateGateway = new();
        private readonly Mock<IDeleteGateway<Expander>> mockedDeleteGateway = new();
        private readonly Mock<IExpanderPluginLoaderInteractor> mockedPluginLoader = new();

        public IExpanderSeederInteractorTests()
        {
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ICreateGateway<Expander>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IDeleteGateway<Expander>>()).Returns(mockedDeleteGateway.Object);
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IExpanderPluginLoaderInteractor>()).Returns(mockedPluginLoader.Object);

            interactor = new ExpanderSeederInteractor(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICreateGateway<Expander>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDeleteGateway<Expander>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IExpanderPluginLoaderInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        [Fact]
        public void SeedOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(2, interactor.SeedOrder);
        }

        [Fact]
        public void ResetOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(4, interactor.ResetOrder);
        }

        [Fact]
        public void Reset_ShouldVerify()
        {
            // arrange
            // act
            interactor.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        [Fact]
        public void Seed_ShouldValidate()
        {
            // assert
            App app = new();
            string folder = "Expanders";
            fakes.GenerationOptions.Setup(x => x.ExpandersFolder).Returns(folder);
            mockedPluginLoader.Setup(x => x.ShallowLoadAllExpanders(folder))
                .Returns(new List<IExpanderInteractor> { GetMockedIExpanderInteractor() });

            // act
            interactor.Seed(app);

            // assert
            mockedPluginLoader.Verify(x => x.ShallowLoadAllExpanders(folder), Times.Once);
            Assert.Equal(app.Expanders.Single().Apps.Single(), app);
            Assert.Single(app.Expanders);
            mockedCreateGateway.Verify(
                x => x.Create(It.Is<Expander>(x =>
                x.Id != Guid.Empty &&
                x.Name == "Name" &&
                x.Order == 1 &&
                x.TemplateFolder == ".Templates")), Times.Once);
        }

        private static IExpanderInteractor GetMockedIExpanderInteractor()
        {
            Mock<IExpanderInteractor> mock = new();
            mock.Setup(x => x.Name).Returns("Name");
            mock.Setup(x => x.Order).Returns(1);

            return mock.Object;
        }
    }
}
