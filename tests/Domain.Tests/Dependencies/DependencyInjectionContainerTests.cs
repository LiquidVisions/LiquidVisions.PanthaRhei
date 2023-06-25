using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.Dependencies
{
    public class DependencyInjectionContainerTests
    {
        private readonly DependencyManager container;

        public DependencyInjectionContainerTests()
        {
            container = new DependencyManager(new ServiceCollection());
        }

        [Fact]
        public void GetServices_CalllBeforeBuildingResolver_ShouldNotReturnEmptyCollection()
        {
            // arrange
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            var result = container.GetAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetServices_AfterRegistration_ShouldResolveCollection()
        {
            // arrange
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            container.Build();
            var result = container.GetAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetServices_AfterRegistration_WithoutBuilding_ShouldResolveCollection()
        {
            // arrange
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass1));
            container.AddTransient(typeof(IFakeInterface), typeof(FakeTestClass2));

            // act
            var result = container.GetAll<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetServices_ShouldResolveCollection()
        {
            // arrange
            FakeTestClass1 fakeTestClass1 = new();
            container.AddSingleton<IFakeInterface>(fakeTestClass1);

            // act
            container.Build();
            var result = container.Get<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Same(result, fakeTestClass1);
        }

        [Fact]
        public void GetServices_WithoutBuilding_ShouldResolveCollection()
        {
            // arrange
            FakeTestClass1 fakeTestClass1 = new();
            container.AddSingleton<IFakeInterface>(fakeTestClass1);

            // act
            var result = container.Get<IFakeInterface>();

            // assert
            Assert.NotNull(result);
            Assert.Same(result, fakeTestClass1);
        }
    }
}
