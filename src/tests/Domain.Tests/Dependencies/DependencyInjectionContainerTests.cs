using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.Dependencies
{
    /// <summary>
    /// Tests for <see cref="DependencyManager"/>.
    /// </summary>
    public class DependencyInjectionContainerTests
    {
        private readonly DependencyManager container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionContainerTests"/> class.
        /// </summary>
        public DependencyInjectionContainerTests()
        {
            container = new DependencyManager(new ServiceCollectionWrapper(new ServiceCollection()));
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.ResolveAll{IFakeInterface}"/>.
        /// Should not return empty collection.
        /// </summary>
        [Fact]
        public void GetServicesCalllBeforeBuildingResolverShouldNotReturnEmptyCollection()
        {
            // arrange
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            System.Collections.Generic.IEnumerable<IFakeInterface> result = container.ResolveAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.ResolveAll{IFakeInterface}"/>.
        /// Should rresolve collection.
        /// </summary>
        [Fact]
        public void GetServicesAfterRegistrationShouldResolveCollection()
        {
            // arrange
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            container.Build();
            IEnumerable<IFakeInterface> result = container.ResolveAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.ResolveAll{IFakeInterface}"/>.
        /// Build is not required
        /// </summary>
        [Fact]
        public void GetServicesAfterRegistrationWithoutBuildingShouldResolveCollection()
        {
            // arrange
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            IEnumerable<IFakeInterface> result = container.ResolveAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.Resolve{IFakeInterface}"/>.
        /// </summary>
        [Fact]
        public void GetServicesShouldResolveCollection()
        {
            // arrange
            FakeTestClass1 fakeTestClass1 = new();
            container.AddSingleton<IFakeInterface>(fakeTestClass1);

            // act
            container.Build();
            IFakeInterface result = container.Resolve<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Same(result, fakeTestClass1);
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.Resolve{IFakeInterface}"/>.
        /// Build is not required.
        /// </summary>
        [Fact]
        public void GetServicesWithoutBuildingShouldResolveCollection()
        {
            // arrange
            FakeTestClass1 fakeTestClass1 = new();
            container.AddSingleton<IFakeInterface>(fakeTestClass1);

            // act
            IFakeInterface result = container.Resolve<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Same(result, fakeTestClass1);
        }
    }
}
