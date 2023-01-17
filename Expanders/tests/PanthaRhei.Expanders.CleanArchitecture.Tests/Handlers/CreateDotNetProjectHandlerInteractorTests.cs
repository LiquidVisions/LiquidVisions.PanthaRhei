using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers
{
    public class CreateDotNetProjectHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly CreateDotNetProjectHandlerInteractor interactor;

        public CreateDotNetProjectHandlerInteractorTests()
        {
            fakes.ConfigureIDependencyFactoryInteractor();

            interactor = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectTemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(2));
        }

        [Fact]
        public void Order_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, interactor.Order);
        }

        [Theory]
        [InlineData(GenerationModes.Default)]
        [InlineData(GenerationModes.Migrate)]
        [InlineData(GenerationModes.Extend)]
        [InlineData(GenerationModes.Deploy)]
        [InlineData(GenerationModes.None)]
        public void CanExecute_ShouldBeFalse(GenerationModes mode)
        {
            // arrange
            fakes.Parameters.Setup(x => x.GenerationMode).Returns(mode);

            // act
            bool result = interactor.CanExecute;

            // assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.Parameters.Setup(x => x.Clean).Returns(clean);

            // act
            bool result = interactor.CanExecute;

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            string name = interactor.Name;

            // assert
            Assert.Equal(nameof(CreateDotNetProjectHandlerInteractor), name);
        }

        [Fact]
        public void Execute_ShouldCreateComponentsAndAddPackagesToOnTheComponents()
        {
            // arrange
            fakes.MockCleanArchitectureExpander();
            Package package = new()
            {
                Id = Guid.NewGuid(),
                Component = fakes.ApiComponent.Object,
                Name = "PackageName",
                Version = "1.0.0",
            };

            fakes.ApiComponent.Setup(x => x.Packages).Returns(new List<Package> { package });

            // act
            interactor.Execute();

            // assert
            fakes.IProjectTemplateInteractor.Verify(x => x.CreateNew(Expanders.CleanArchitecture.Resources.TemplateShortName), Times.Once);
            fakes.IProjectTemplateInteractor.Verify(x => x.ApplyPackageOnComponent(fakes.ApiComponent.Object, package), Times.Once);
        }
    }
}
