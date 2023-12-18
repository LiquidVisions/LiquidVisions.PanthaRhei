using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Infrastructure
{
    public class ExpandEntityDatabaseConfigurationHandlerInteractorTests
    {
        private readonly ExpandEntityDatabaseConfigurationTask handler;
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly List<Entity> allEntities;

        public ExpandEntityDatabaseConfigurationHandlerInteractorTests()
        {
            allEntities = fakes.GetValidEntities();
            fakes.MockCleanArchitectureExpander(allEntities);
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<ITemplate>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(4));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(8, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandEntityDatabaseConfigurationTask), handler.Name);
        }

        [Theory]
        [InlineData(GenerationModes.Default, true)]
        [InlineData(GenerationModes.Migrate, false)]
        [InlineData(GenerationModes.Extend, true)]
        [InlineData(GenerationModes.Deploy, false)]
        [InlineData(GenerationModes.None, false)]
        public void CanExecute_ShouldBeFalse(GenerationModes mode, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        [Fact]
        public void Execute_ShouldRenderAndSave()
        {
            // arranges
            App app = fakes.SetupApp();
            string ns = fakes.InfrastructureComponent.Object.GetComponentNamespace(app);
            string entityNs = fakes.DomainComponent.Object.GetComponentNamespace(app, Expanders.CleanArchitecture.Resources.DomainEntityFolder);

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(Path.Combine(fakes.ExpectedCompontentOutputFolder, Expanders.CleanArchitecture.Resources.InfrastructureConfigurationFolder)), Times.Once);
            fakes.ITemplate.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(allEntities.Count));

            foreach (Entity entity in allEntities)
            {
                string fullSavePath = Path.Combine(
                    fakes.CleanArchitectureExpander.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object),
                    Expanders.CleanArchitecture.Resources.InfrastructureConfigurationFolder,
                    $"{entity.Name}Configuration.cs");

                var indexes = entity.Fields.Where(x => x.IsIndex).Select(x => x.Name).ToArray();
                var keys = entity.Fields.OrderBy(x => x.Order).Where(x => x.IsKey).Select(x => x.Name).ToArray();

                fakes.ITemplate.Verify(
                    x => x.RenderAndSave(
                        It.IsAny<string>(),
                        It.Is<object>(x =>
                        VerifyHelpers.AreEqualObjects(x, new
                        {
                            Entity = entity,
                            NameSpace = ns,
                            EntityNameSpace = entityNs,
                            Indexes = indexes,
                            Keys = keys,
                        })),
                        fullSavePath),
                    Times.Once);
            }
        }
    }
}
