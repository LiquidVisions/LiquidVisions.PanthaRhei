using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Seeders
{
    public class ExpanderSeederInteractorTests
    {
        private readonly ExpanderSeederInteractor interactor;
        private readonly Fakes fakes = new();
        private readonly Mock<IDeleteGateway<Expander>> deleteGateWay = new();
        private readonly Mock<ICreateGateway<Expander>> createGateWay = new();

        public ExpanderSeederInteractorTests()
        {
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IDeleteGateway<Expander>>()).Returns(deleteGateWay.Object);
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ICreateGateway<Expander>>()).Returns(createGateWay.Object);

            interactor = new(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDeleteGateway<Expander>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICreateGateway<Expander>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IExpanderPluginLoaderInteractor>(), Times.Once);
        }

        [Fact]
        public void SeedOrder_ShouldBe()
        {
            // arrange
            // act
            // assert
            Assert.Equal(2, interactor.SeedOrder);
        }

        [Fact]
        public void ResetOrder_ShouldBe()
        {
            // arrange
            // act
            // assert
            Assert.Equal(4, interactor.ResetOrder);
        }

        [Fact]
        public void Reset_ShouldDeleteAll()
        {
            // arrange

            // act
            interactor.Reset();

            // assert
            deleteGateWay.Verify(x => x.DeleteAll(), Times.Once);
        }

        [Fact]
        public void Seed_ShouldWhatEver()
        {
            // arrange
            App app = new();
            Mock<IExpanderInteractor> mockedExpanderInteractor = new Mock<IExpanderInteractor>();
            mockedExpanderInteractor.Setup(x => x.Name).Returns("RandomName");
            mockedExpanderInteractor.Setup(x => x.Order).Returns(2);
            fakes.IExpanderPluginLoaderInteractor.Setup(x => x.ShallowLoadAllExpanders(fakes.Parameters.Object.ExpandersFolder)).Returns(new List<IExpanderInteractor> { mockedExpanderInteractor.Object });

            // act
            interactor.Seed(app);

            // assert
            fakes.IExpanderPluginLoaderInteractor.Verify(x => x.ShallowLoadAllExpanders(fakes.Parameters.Object.ExpandersFolder), Times.Once);
            Assert.Single(app.Expanders);
            Assert.Same(app.Expanders.Single().Apps.Single(), app);
            createGateWay.Verify(x => x.Create(It.IsAny<Expander>()), Times.Once);
            createGateWay.Verify(x => x.Create(It.Is<Expander>(x => x.Id != Guid.Empty && x.Name == "RandomName" && x.Order == 2 && x.TemplateFolder == ".Templates" && x.Apps.Single() == app)), Times.Once);
        }
    }
}
