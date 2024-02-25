using System;
using System.IO;
using System.Xml.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <see cref="PackageSeeder"/>.
    /// </summary>
    public class PackageSeederTests
    {
        private readonly ApplicationFakes fakes = new();
        private readonly PackageSeeder seeder;
        private readonly Mock<ICreateRepository<Package>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<Package>> mockedDeleteGateway = new();
        private readonly Mock<IXDocument> mockedXDocument = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageSeederTests"/> class.
        /// </summary>
        public PackageSeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Package>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Package>>()).Returns(mockedDeleteGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IXDocument>()).Returns(mockedXDocument.Object);

            seeder = new PackageSeeder(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Package>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Package>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IXDocument>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(5));
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
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
            Assert.Equal(4, seeder.SeedOrder);
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
            Assert.Equal(2, seeder.ResetOrder);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            seeder.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
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
            Assert.Throws<ArgumentNullException>(() => seeder.Seed(null));
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
            string templatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, component.Expander.Name, Resources.TemplatesFolder);
            fakes.IDirectory.Setup(x => x.Exists(templatePath)).Returns(true);
            fakes.IDirectory.Setup(x => x.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories)).Returns([file]);

            string xmlString = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <ItemGroup>
    <PackageReference Update=""Test.Package"" Version=""4.8.0"" />
  </ItemGroup>

</Project>";

            XDocument xml = XDocument.Parse(xmlString);
            mockedXDocument.Setup(x => x.Load(file)).Returns(xml);


            // act
            seeder.Seed(app);

            // assert
            fakes.IDirectory.Verify(x => x.Exists(templatePath), Times.Once);
            fakes.IDirectory.Verify(x => x.GetFiles(templatePath, "*.csproj", SearchOption.AllDirectories), Times.Once);
            mockedCreateGateway.Verify(x => x.Create(It.Is<Package>(x => x.Id != Guid.Empty && x.Name == "Test.Package" && x.Version == "4.8.0")), Times.Once);
        }
    }
}
