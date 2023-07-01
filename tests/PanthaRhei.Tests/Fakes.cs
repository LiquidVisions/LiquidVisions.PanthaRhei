using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Moq;

namespace LiquidVisions.PanthaRhei.Tests
{
    public class Fakes
    {
        public Fakes()
        {
            Configure();
            ConfigureIDependencyFactory();
        }

        public Mock<IDependencyFactory> IDependencyFactory { get; } = new();

        public Mock<IDependencyManager> IDependencyManager { get; } = new();

        public Mock<ILogger> ILogger { get; } = new();

        public Mock<ILogManager> ILogManager { get; } = new();

        public Mock<IFile> IFile { get; } = new();

        public Mock<IDirectory> IDirectory { get; } = new();

        public Mock<GenerationOptions> GenerationOptions { get; } = new();

        public Mock<IExpanderDependencyManager> IExpanderDependencyManager { get; } = new();

        public Mock<IWriter> IWriter { get; } = new();

        public Mock<ITemplate> ITemplate { get; } = new();

        public Mock<ICommandLine> ICommandLine { get; } = new();

        internal Mock<IAssemblyManager> IAssemblyManager { get; } = new();

        public virtual void Configure()
        {
            ILogManager.Setup(x => x.GetExceptionLogger()).Returns(ILogger.Object);

            GenerationOptions.Setup(x => x.Root).Returns("C:\\Some\\Root\\Folder");
            GenerationOptions.Setup(x => x.OutputFolder).Returns("C:\\Some\\Root\\OutputFolder");
            GenerationOptions.Setup(x => x.ExpandersFolder).Returns("C:\\Some\\Root\\Expanders");
            GenerationOptions.Setup(x => x.HarvestFolder).Returns("C:\\Some\\Root\\HarvestFolder");
        }

        public virtual void ConfigureIDependencyFactory()
        {
            IDependencyFactory.Setup(x => x.Get<ILogger>()).Returns(ILogger.Object);
            IDependencyFactory.Setup(x => x.Get<ILogManager>()).Returns(ILogManager.Object);
            IDependencyFactory.Setup(x => x.Get<IDependencyManager>()).Returns(IDependencyManager.Object);
            IDependencyFactory.Setup(x => x.Get<IFile>()).Returns(IFile.Object);
            IDependencyFactory.Setup(x => x.Get<IDirectory>()).Returns(IDirectory.Object);
            IDependencyFactory.Setup(x => x.Get<GenerationOptions>()).Returns(GenerationOptions.Object);
            IDependencyFactory.Setup(x => x.Get<IDependencyManager>()).Returns(IDependencyManager.Object);
            IDependencyFactory.Setup(x => x.Get<IAssemblyManager>()).Returns(IAssemblyManager.Object);
            IDependencyFactory.Setup(x => x.Get<IWriter>()).Returns(IWriter.Object);
            IDependencyFactory.Setup(x => x.Get<ITemplate>()).Returns(ITemplate.Object);
            IDependencyFactory.Setup(x => x.Get<ICommandLine>()).Returns(ICommandLine.Object);
        }
    }
}
