using System;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases
{
    /// <summary>
    /// Tests for <see cref="DependencyManager"/>.
    /// </summary>
    public class DependencyManagerTests
    {
        /// <summary>
        /// Test for <see cref="DependencyManager.AddTransient(Type, Type)"/>.
        /// </summary>
        [Fact]
        public void AddTransientShouldVerify()
        {
            // arrange
            Mock<Type> serviceType = new();
            Mock<Type> implementationType = new();

            Mock<IServiceCollectionWrapper> collection = new();
            DependencyManager manager = new(collection.Object);

            // act
            manager.AddTransient(serviceType.Object, implementationType.Object);

            // assert
            collection.Verify(x => x.AddTransient(serviceType.Object, implementationType.Object), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.AddSingleton(Type, Type)"/>.
        /// </summary>
        [Fact]
        public void AddSingleTonShouldVerify()
        {
            // arrange
            Mock<Type> serviceType = new();
            Mock<Type> implementationType = new();

            Mock<IServiceCollectionWrapper> collection = new();
            DependencyManager manager = new(collection.Object);

            // act
            manager.AddSingleton(serviceType.Object, implementationType.Object);

            // assert
            collection.Verify(x => x.AddSingleton(serviceType.Object, implementationType.Object), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.AddSingleton(Type, Type)"/>.
        /// </summary>
        [Fact]
        public void AddSingleTonV2ShouldVerify()
        {
            // arrange
            Mock<Type> serviceType = new();
            Mock<Type> implementationType = new();

            Mock<IServiceCollectionWrapper> collection = new();
            DependencyManager manager = new(collection.Object);

            // act
            manager.AddSingleton(implementationType.Object);

            // assert
            collection.Verify(x => x.AddSingleton(implementationType.Object), Times.Once);
        }

    }
}
