﻿using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Harvesters
{
    public class MigrationHarvesterInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly MigrationHarvesterInteractor interactor;
        private readonly string expectedMigrationsFolder;

        public MigrationHarvesterInteractorTests()
        {
            fakes.MockCleanArchitectureExpander();

            interactor = new MigrationHarvesterInteractor(fakes.IDependencyFactoryInteractor.Object);

            expectedMigrationsFolder = System.IO.Path.Combine(fakes.Parameters.Object.OutputFolder, CleanArchitectureResources.InfrastructureMigrationsFolder);
        }

        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IHarvestSerializerInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<CleanArchitectureExpander>(), Times.Once);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void CanExecute_ShouldValidate(bool folderExists, bool canExecuteResult)
        {
            // arranges
            fakes.IDirectory.Setup(x => x.Exists(expectedMigrationsFolder)).Returns(folderExists);

            // act
            bool result = interactor.CanExecute;

            // assert
            fakes.IDirectory.Verify(x => x.Exists(expectedMigrationsFolder), Times.Once);
            fakes.Parameters.Verify(x => x.GenerationMode, Times.Never);
            Assert.Equal(canExecuteResult, result);
        }

        [Fact]
        public void Execute_ShouldDeserialze()
        {
            // arrange
            string basePath = "C:\\Path\\To\\Some";
            fakes.Parameters.Setup(x => x.HarvestFolder).Returns("C:\\Output\\SomeHarvestFolder");
            string sourceFile1 = $"{basePath}\\SourceFile1";
            string sourceFile2 = $"{basePath}\\SourceFile2";

            string fullPathHarvestFile1 = Path.Combine(fakes.Parameters.Object.HarvestFolder, fakes.CleanArchitectureExpander.Object.Model.Name, $"{sourceFile1}.{CleanArchitectureResources.MigrationHarvesterExtensionFile}");
            string fullPathHarvestFile2 = Path.Combine(fakes.Parameters.Object.HarvestFolder, fakes.CleanArchitectureExpander.Object.Model.Name, $"{sourceFile2}.{CleanArchitectureResources.MigrationHarvesterExtensionFile}");


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
            fakes.IHarvestSerializerInteractor.Verify(x => x.Deserialize(It.IsAny<Harvest>(), It.IsAny<string>()), Times.Exactly(2));
            fakes.IHarvestSerializerInteractor.Verify(
                x => x.Deserialize(
                    It.Is<Harvest>(h => h.Path == $"{sourceFile1}.cs" && h.Items.Count == 1 && h.Items.First().Content == contentFile1),
                    fullPathHarvestFile1),
                Times.Once);

            fakes.IHarvestSerializerInteractor.Verify(
                x => x.Deserialize(
                    It.Is<Harvest>(h => h.Path == $"{sourceFile2}.cs" && h.Items.Count == 1 && h.Items.First().Content == contentFile2),
                    fullPathHarvestFile2),
                Times.Once);
        }
    }
}