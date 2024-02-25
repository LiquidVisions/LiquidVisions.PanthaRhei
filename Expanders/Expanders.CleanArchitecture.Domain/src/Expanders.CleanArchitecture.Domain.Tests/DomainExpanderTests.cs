using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Domain.Tests
{
    public class DomainExpanderTests
    {
        private readonly DomainExpanderFakes _fakes = new();
        private readonly DomainExpander _expander;

        public DomainExpanderTests()
        {
            App app = new ();
            Expander expander = new ()
            {
                Name = "CleanArchitecture.Domain",
            };
            app.Expanders.Add(expander);

            _fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(app);
            _expander = new DomainExpander(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(3));
        }

        [Fact]
        public void NameShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal("CleanArchitecture.Domain", _expander.Name);
        }
    }
}
