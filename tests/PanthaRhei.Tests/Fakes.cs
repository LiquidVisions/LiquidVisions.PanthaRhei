using LiquidVisions.PanthaRhei.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Interactors;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Initializers;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using Moq;

namespace LiquidVisions.PanthaRhei.Tests
{
    public class Fakes
    {
        public Fakes()
        {
            Configure();
            ConfigureIDependencyFactoryInteractor();
        }

        public Mock<IDependencyFactory> IDependencyFactoryInteractor { get; } = new();

        public Mock<IDependencyManager> IDependencyManagerInteractor { get; } = new();

        public Mock<ICodeGeneratorBuilderInteractor> ICodeGeneratorBuilderInteractor { get; } = new();

        public Mock<ILogger> ILogger { get; } = new();

        public Mock<ILogManager> ILogManager { get; } = new();

        public Mock<ICodeGeneratorInteractor> ICodeGeneratorInteractor { get; } = new();

        public Mock<IFile> IFile { get; } = new();

        public Mock<IDirectory> IDirectory { get; } = new();

        public Mock<GenerationOptions> GenerationOptions { get; } = new();

        public Mock<IExpanderDependencyManager> IExpanderDependencyManagerInteractor { get; } = new();

        public Mock<IExpanderPluginLoaderInteractor> IExpanderPluginLoaderInteractor { get; } = new();

        public Mock<IWriterInteractor> IWriterInteractor { get; } = new();

        public Mock<ITemplateInteractor> ITemplateInteractor { get; } = new();

        public Mock<ICommandLineInteractor> ICommandLineInteractor { get; } = new();

        internal Mock<IAssemblyContextInteractor> IAssemblyContextInteractor { get; } = new();

        internal Mock<IObjectActivatorInteractor> IObjectActivatorInteractor { get; } = new();

        internal Mock<IAssemblyManagerInteractor> IAssemblyManagerInteractor { get; } = new();

        public void Configure()
        {
            ILogManager.Setup(x => x.GetExceptionLogger()).Returns(ILogger.Object);
            ICodeGeneratorBuilderInteractor.Setup(x => x.Build()).Returns(ICodeGeneratorInteractor.Object);
            GenerationOptions.Setup(x => x.Root).Returns("C:\\Some\\Root\\Folder");
            GenerationOptions.Setup(x => x.OutputFolder).Returns("C:\\Some\\Root\\OutputFolder");
            GenerationOptions.Setup(x => x.ExpandersFolder).Returns("C:\\Some\\Root\\Expanders");
            GenerationOptions.Setup(x => x.HarvestFolder).Returns("C:\\Some\\Root\\HarvestFolder");
        }

        public virtual void ConfigureIDependencyFactoryInteractor()
        {
            IDependencyFactoryInteractor.Setup(x => x.Get<ILogger>()).Returns(ILogger.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<ILogManager>()).Returns(ILogManager.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<ICodeGeneratorInteractor>()).Returns(ICodeGeneratorInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IDependencyManager>()).Returns(IDependencyManagerInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<ICodeGeneratorBuilderInteractor>()).Returns(ICodeGeneratorBuilderInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IFile>()).Returns(IFile.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IDirectory>()).Returns(IDirectory.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<GenerationOptions>()).Returns(GenerationOptions.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IAssemblyContextInteractor>()).Returns(IAssemblyContextInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IObjectActivatorInteractor>()).Returns(IObjectActivatorInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IDependencyManager>()).Returns(IDependencyManagerInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IAssemblyManagerInteractor>()).Returns(IAssemblyManagerInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IExpanderPluginLoaderInteractor>()).Returns(IExpanderPluginLoaderInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<IWriterInteractor>()).Returns(IWriterInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<ITemplateInteractor>()).Returns(ITemplateInteractor.Object);
            IDependencyFactoryInteractor.Setup(x => x.Get<ICommandLineInteractor>()).Returns(ICommandLineInteractor.Object);
        }
    }
}
