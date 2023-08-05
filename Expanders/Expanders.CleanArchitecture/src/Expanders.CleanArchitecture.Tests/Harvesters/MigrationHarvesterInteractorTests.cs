using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Harvesters;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Harvesters
{
    public class MigrationHarvesterInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly MigrationHarvester interactor;
        private readonly string expectedMigrationsFolder;
        private readonly Mock<ICreateRepository<Harvest>> mockedICreateGateWay = new ();

        public MigrationHarvesterInteractorTests()
        {
            fakes.MockCleanArchitectureExpander();
            fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<Harvest>>()).Returns(mockedICreateGateWay.Object);

            interactor = new MigrationHarvester(fakes.IDependencyFactory.Object);

            expectedMigrationsFolder = System.IO.Path.Combine(fakes.GenerationOptions.Object.OutputFolder, CleanArchitectureResources.InfrastructureMigrationsFolder);
        }

        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<Harvest>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<CleanArchitectureExpander>(), Times.Once);
        }

        [Theory]
        [InlineData(GenerationModes.Harvest, true, true)]
        [InlineData(GenerationModes.Harvest | GenerationModes.Default, true, true)]
        [InlineData(GenerationModes.Default, true, false)]
        [InlineData(GenerationModes.Harvest, false, false)]
        public void CanExecute_ShouldValidate(GenerationModes modes, bool folderExists, bool canExecuteResult)
        {
            // arranges
            fakes.IDirectory.Setup(x => x.Exists(expectedMigrationsFolder)).Returns(folderExists);
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(modes);

            // act
            bool result = interactor.Enabled;

            // assert
            fakes.GenerationOptions.Verify(x => x.Modes, Times.Once);
            Assert.Equal(canExecuteResult, result);
        }

        [Fact]
        public void Execute_ShouldDeserialze()
        {
            // arrange
            string basePath = "C:\\Path\\To\\Some";
            fakes.GenerationOptions.Setup(x => x.HarvestFolder).Returns("C:\\Output\\SomeHarvestFolder");
            string sourceFile1 = $"{basePath}\\SourceFile1";
            string sourceFile2 = $"{basePath}\\SourceFile2";

            string contentFile1 = "ContentFile1";
            string contentFile2 = "ContentFile2";

            fakes.IFile.Setup(x => x.ReadAllText($"{sourceFile1}.cs")).Returns(contentFile1);
            fakes.IFile.Setup(x => x.ReadAllText($"{sourceFile2}.cs")).Returns(contentFile2);

            fakes.IDirectory.Setup(
                x => x.GetFiles(
                    expectedMigrationsFolder,
                    "*.cs",
                    System.IO.SearchOption.AllDirectories))
                .Returns(new string[] { $"{sourceFile1}.cs", $"{sourceFile2}.cs" });
            fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{sourceFile1}.cs")).Returns(sourceFile1);
            fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{sourceFile2}.cs")).Returns(sourceFile2);

            // act
            interactor.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.GetFiles(expectedMigrationsFolder, "*.cs", System.IO.SearchOption.AllDirectories), Times.Once);
            mockedICreateGateWay.Verify(x => x.Create(It.IsAny<Harvest>()), Times.Exactly(2));

            mockedICreateGateWay.Verify(
                x => x.Create(It.Is<Harvest>(h => h.Path == $"{sourceFile1}.cs" && h.Items.Count == 1 && h.Items.First().Content == contentFile1 && h.HarvestType == Expanders.CleanArchitecture.Resources.MigrationHarvesterExtensionFile)),
                Times.Once);

            mockedICreateGateWay.Verify(
                x => x.Create(It.Is<Harvest>(h => h.Path == $"{sourceFile2}.cs" && h.Items.Count == 1 && h.Items.First().Content == contentFile2 && h.HarvestType == Expanders.CleanArchitecture.Resources.MigrationHarvesterExtensionFile)),
                Times.Once);
        }
    }
}
