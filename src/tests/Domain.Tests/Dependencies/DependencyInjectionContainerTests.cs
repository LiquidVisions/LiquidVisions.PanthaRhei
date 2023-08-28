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
        private readonly DependencyManager _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionContainerTests"/> class.
        /// </summary>
        public DependencyInjectionContainerTests()
        {
            _container = new DependencyManager(new ServiceCollection());
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.GetAll{IFakeInterface}"/>.
        /// Should not return empty collection.
        /// </summary>
        [Fact]
        public void GetServices_CalllBeforeBuildingResolver_ShouldNotReturnEmptyCollection()
        {
            // arrange
            _container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            _container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            System.Collections.Generic.IEnumerable<IFakeInterface> result = _container.GetAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.GetAll{IFakeInterface}"/>.
        /// Should rresolve collection.
        /// </summary>
        [Fact]
        public void GetServices_AfterRegistration_ShouldResolveCollection()
        {
            // arrange
            _container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            _container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            _container.Build();
            IEnumerable<IFakeInterface> result = _container.GetAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.GetAll{IFakeInterface}"/>.
        /// Build is not required
        /// </summary>
        [Fact]
        public void GetServices_AfterRegistration_WithoutBuilding_ShouldResolveCollection()
        {
            // arrange
            _container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            _container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            IEnumerable<IFakeInterface> result = _container.GetAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.Get{IFakeInterface}"/>.
        /// </summary>
        [Fact]
        public void GetServices_ShouldResolveCollection()
        {
            // arrange
            FakeTestClass1 fakeTestClass1 = new();
            _container.AddSingleton<IFakeInterface>(fakeTestClass1);

            // act
            _container.Build();
            IFakeInterface result = _container.Get<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Same(result, fakeTestClass1);
        }

        /// <summary>
        /// Test for <see cref="DependencyManager.Get{IFakeInterface}"/>.
        /// Build is not required.
        /// </summary>
        [Fact]
        public void GetServices_WithoutBuilding_ShouldResolveCollection()
        {
            // arrange
            FakeTestClass1 fakeTestClass1 = new();
            _container.AddSingleton<IFakeInterface>(fakeTestClass1);

            // act
            IFakeInterface result = _container.Get<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Same(result, fakeTestClass1);
        }
    }
}
