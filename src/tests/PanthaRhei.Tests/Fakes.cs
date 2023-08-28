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
    /// <summary>
    /// Mock objects for unit tests.
    /// </summary>
    public class Fakes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fakes"/> class.
        /// </summary>
        public Fakes()
        {
            Configure();
            ConfigureIDependencyFactory();
        }

        /// <summary>
        /// Mock for <see cref="IDependencyFactory"/>.
        /// </summary>
        public Mock<IDependencyFactory> IDependencyFactory { get; } = new();

        /// <summary>
        /// Mock for <see cref="IDependencyManager"/>.
        /// </summary>
        public Mock<IDependencyManager> IDependencyManager { get; } = new();

        /// <summary>
        /// Mock for <see cref="ILogger"/>.
        /// </summary>
        public Mock<ILogger> ILogger { get; } = new();

        /// <summary>
        /// Mock for <see cref="ILogManager"/>.
        /// </summary>
        public Mock<ILogManager> ILogManager { get; } = new();

        /// <summary>
        /// Mock for <see cref="IFile"/>.
        /// </summary>
        public Mock<IFile> IFile { get; } = new();

        /// <summary>
        /// Mock for <see cref="IDirectory"/>.
        /// </summary>
        public Mock<IDirectory> IDirectory { get; } = new();

        /// <summary>
        /// Mock for <seealso cref="GenerationOptions"/>.
        /// </summary>
        public Mock<GenerationOptions> GenerationOptions { get; } = new();

        /// <summary>
        /// Mock for <see cref="IExpanderDependencyManager"/>.
        /// </summary>
        public Mock<IExpanderDependencyManager> IExpanderDependencyManager { get; } = new();

        /// <summary>
        /// Mock for <see cref="IWriter"/>.
        /// </summary>
        public Mock<IWriter> IWriter { get; } = new();

        /// <summary>
        /// Mock for <see cref="ITemplate"/>.
        /// </summary>
        public Mock<ITemplate> ITemplate { get; } = new();

        /// <summary>
        /// Mock for <see cref="ICommandLine"/>.
        /// </summary>
        public Mock<ICommandLine> ICommandLine { get; } = new();

        /// <summary>
        /// Mock for <see cref="IAssemblyManager"/>.
        /// </summary>
        internal Mock<IAssemblyManager> IAssemblyManager { get; } = new();

        /// <summary>
        /// Configures the mock objects.
        /// </summary>
        public virtual void Configure()
        {
            ILogManager.Setup(x => x.GetExceptionLogger()).Returns(ILogger.Object);

            GenerationOptions.Setup(x => x.Root).Returns("C:\\Some\\Root\\Folder");
            GenerationOptions.Setup(x => x.OutputFolder).Returns("C:\\Some\\Root\\OutputFolder");
            GenerationOptions.Setup(x => x.ExpandersFolder).Returns("C:\\Some\\Root\\Expanders");
            GenerationOptions.Setup(x => x.HarvestFolder).Returns("C:\\Some\\Root\\HarvestFolder");
        }

        /// <summary>
        /// Configures the <see cref="IDependencyFactory"/>.
        /// </summary>
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
