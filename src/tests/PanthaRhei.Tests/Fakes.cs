using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Domain.Usecases.UpdatePackages;
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
#pragma warning disable CA2214 // Do not call overridable methods in constructors
            Configure();
            ConfigureIDependencyFactory();
#pragma warning restore CA2214 // Do not call overridable methods in constructors

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
        /// Mock for <see cref="ICreateNewExpander"/>.
        /// </summary>
        public Mock<ICreateNewExpander> ICreateNewExpander  { get;} = new ();

        /// <summary>
        /// Mock for <see cref="IUpdatePackages"/>.
        /// </summary>
        public Mock<IUpdatePackagesUseCase> IUpdatePackages { get; } = new();

        /// <summary>
        /// Mock for <see cref="IApplication"/>.
        /// </summary>
        public Mock<IApplication> IApplication { get; } = new();

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
            IDependencyFactory.Setup(x => x.Resolve<ILogger>()).Returns(ILogger.Object);
            IDependencyFactory.Setup(x => x.Resolve<ILogManager>()).Returns(ILogManager.Object);
            IDependencyFactory.Setup(x => x.Resolve<IDependencyManager>()).Returns(IDependencyManager.Object);
            IDependencyFactory.Setup(x => x.Resolve<IFile>()).Returns(IFile.Object);
            IDependencyFactory.Setup(x => x.Resolve<IDirectory>()).Returns(IDirectory.Object);
            IDependencyFactory.Setup(x => x.Resolve<GenerationOptions>()).Returns(GenerationOptions.Object);
            IDependencyFactory.Setup(x => x.Resolve<IDependencyManager>()).Returns(IDependencyManager.Object);
            IDependencyFactory.Setup(x => x.Resolve<IAssemblyManager>()).Returns(IAssemblyManager.Object);
            IDependencyFactory.Setup(x => x.Resolve<IWriter>()).Returns(IWriter.Object);
            IDependencyFactory.Setup(x => x.Resolve<ITemplate>()).Returns(ITemplate.Object);
            IDependencyFactory.Setup(x => x.Resolve<ICommandLine>()).Returns(ICommandLine.Object);
            IDependencyFactory.Setup(x => x.Resolve<IExpanderDependencyManager>()).Returns(IExpanderDependencyManager.Object);
            IDependencyFactory.Setup(x => x.Resolve<IApplication>()).Returns(IApplication.Object);
            IDependencyFactory.Setup(x => x.Resolve<ICreateNewExpander>()).Returns(ICreateNewExpander.Object);
            IDependencyFactory.Setup(x => x.Resolve<IUpdatePackagesUseCase>()).Returns(IUpdatePackages.Object);
        }
    }
}
