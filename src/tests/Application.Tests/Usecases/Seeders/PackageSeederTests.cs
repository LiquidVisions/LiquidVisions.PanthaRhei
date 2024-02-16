using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <see cref="PackageSeeder"/>.
    /// </summary>
    public class PackageSeederTests
    {
        private readonly ApplicationFakes _fakes = new();
        private readonly PackageSeeder _seeder;
        private readonly Mock<ICreateRepository<Package>> _mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<Package>> _mockedDeleteGateway = new();
        private readonly Mock<IXDocument> _mockedXDocument = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageSeederTests"/> class.
        /// </summary>
        public PackageSeederTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Package>>()).Returns(_mockedCreateGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Package>>()).Returns(_mockedDeleteGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IXDocument>()).Returns(_mockedXDocument.Object);

            _seeder = new PackageSeeder(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Package>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Package>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IXDocument>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(5));
            _fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe4()
        {
            // arrange
            // act
            // assert
            Assert.Equal(4, _seeder.SeedOrder);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe2()
        {
            // arrange
            // act
            // assert
            Assert.Equal(2, _seeder.ResetOrder);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            _seeder.Reset();

            // assert
            _mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Tests null reference exception when package seeder initialization is done with null reference.
        /// </summary>
        [Fact]
        public void ShouldThrowNullWhenPackageSeerderInitializationIsDoneWithNullReference()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => new PackageSeeder(null));
        }

        /// <summary>
        /// Tests null reference exception on <seealso cref="PackageSeeder.Seed(App)"/>.
        /// </summary>
        [Fact]
        public void ShouldThrowNullWhenWhenArgumentPassedInIsNull()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => _seeder.Seed(null));
        }

        /// <summary>
        /// Tests that the package seeder does not seed when the template path does not exist.
        /// </summary>
        [Fact]
        public void SeedShouldCreatePackageReference()
        {
            // arrange
            Expander expander = new()
            {
                Name = "TestExpander"
            };

            Component component = new()
            {
                Expander = expander
            };
            expander.Components.Add(component);

            App app = new();
            app.Expanders.Add(expander);

            string file = "test.csproj";
            string templatePath = Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, component.Expander.Name, Resources.TemplatesFolder);
            _fakes.IDirectory.Setup(x => x.Exists(templatePath)).Returns(true);
            _fakes.IDirectory.Setup(x => x.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories)).Returns([file]);

            string xmlString = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <ItemGroup>
    <PackageReference Update=""Test.Package"" Version=""4.8.0"" />
  </ItemGroup>

</Project>";

            XDocument xml = XDocument.Parse(xmlString);
            _mockedXDocument.Setup(x => x.Load(file)).Returns(xml);


            // act
            _seeder.Seed(app);

            // assert
            _fakes.IDirectory.Verify(x => x.Exists(templatePath), Times.Once);
            _fakes.IDirectory.Verify(x => x.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories), Times.Once);
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Package>(x => x.Id != Guid.Empty && x.Name == "Test.Package" && x.Version == "4.8.0")), Times.Once);
        }
    }
}
