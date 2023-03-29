using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generator.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Boundaries
{
    public class SeedingBoundaryTests
    {
        private readonly SeedingBoundary boundary;
        private readonly Fakes fakes = new();
        private readonly Mock<IEntitySeederInteractor<App>> mockedSeederInteractor;

        public SeedingBoundaryTests()
        {
            mockedSeederInteractor = new Mock<IEntitySeederInteractor<App>>();

            fakes.IDependencyFactoryInteractor
                .Setup(x => x.GetAll<IEntitySeederInteractor<App>>())
                .Returns(new List<IEntitySeederInteractor<App>> { mockedSeederInteractor.Object });

            boundary = new SeedingBoundary(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Execute_HappyFlow_ShouldVeryfy()
        {
            // arrange
            // act
            boundary.Execute();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IEntitySeederInteractor<App>>(), Times.Once);
            mockedSeederInteractor.Verify(x => x.Reset(), Times.Once);
            mockedSeederInteractor.Verify(x => x.Seed(It.IsAny<App>()), Times.Once);
        }
    }
}
