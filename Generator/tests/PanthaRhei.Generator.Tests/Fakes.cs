using System;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using Moq;

namespace LiquidVisions.PanthaRhei.Generator.Tests
{
    public class Fakes
    {
        public Fakes()
        {
            IDependencyFactoryInteractor = new();

            ILogger = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<ILogger>())
                .Returns(ILogger.Object);
            ILogManager = new();
            ILogManager.Setup(x => x.GetExceptionLogger())
                .Returns(ILogger.Object);
            IDependencyFactoryInteractor
                .Setup(x => x.Get<ILogManager>())
                .Returns(ILogManager.Object);

            ICodeGeneratorInteractor = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<ICodeGeneratorInteractor>())
                .Returns(ICodeGeneratorInteractor.Object);

            IDependencyManagerInteractor = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<IDependencyManagerInteractor>())
                .Returns(IDependencyManagerInteractor.Object);

            ICodeGeneratorBuilderInteractor = new();
            ICodeGeneratorBuilderInteractor
                .Setup(x => x.Build())
                .Returns(ICodeGeneratorInteractor.Object);
            IDependencyFactoryInteractor
                .Setup(x => x.Get<ICodeGeneratorBuilderInteractor>())
                .Returns(ICodeGeneratorBuilderInteractor.Object);

            IFile = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<IFile>())
                .Returns(IFile.Object);

            IDirectory = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<IDirectory>())
                .Returns(IDirectory.Object);

            Parameters = new();
            Parameters.Setup(x => x.Root).Returns(@"C:\\Some\\Root\\Folder\");
            Parameters.Setup(x => x.OutputFolder).Returns(@"C:\\Some\\Root\\OutputFolder\");
            Parameters.Setup(x => x.ExpandersFolder).Returns(@"C:\\Some\\Root\\Expanders\");
            IDependencyFactoryInteractor
                .Setup(x => x.Get<Parameters>())
                .Returns(Parameters.Object);

            IAssemblyContextInteractor = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<IAssemblyContextInteractor>())
                .Returns(IAssemblyContextInteractor.Object);

            IObjectActivatorInteractor = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<IObjectActivatorInteractor>())
                .Returns(IObjectActivatorInteractor.Object);

            IDependencyManagerInteractor = new();
            IDependencyFactoryInteractor
                .Setup(x => x.Get<IDependencyManagerInteractor>())
                .Returns(IDependencyManagerInteractor.Object);

            IExpanderDependencyManagerInteractor = new();

            IAssemblyManagerInteractor = new();
            IDependencyFactoryInteractor
            .Setup(x => x.Get<IAssemblyManagerInteractor>())
            .Returns(IAssemblyManagerInteractor.Object);

            IAppGateway = new();
            IDependencyFactoryInteractor
            .Setup(x => x.Get<IGenericGateway<App>>())
            .Returns(IAppGateway.Object);

            IExpanderPluginLoaderInteractor = new();
            IDependencyFactoryInteractor
            .Setup(x => x.Get<IExpanderPluginLoaderInteractor>())
            .Returns(IExpanderPluginLoaderInteractor.Object);
        }

        public Mock<IDependencyFactoryInteractor> IDependencyFactoryInteractor { get; }

        public Mock<IDependencyManagerInteractor> IDependencyManagerInteractor { get; }

        public Mock<ICodeGeneratorBuilderInteractor> ICodeGeneratorBuilderInteractor { get; }

        public Mock<ILogger> ILogger { get; }

        public Mock<ILogManager> ILogManager { get; }

        public Mock<ICodeGeneratorInteractor> ICodeGeneratorInteractor { get; }

        public Mock<IFile> IFile { get; }

        public Mock<IDirectory> IDirectory { get; }

        public Mock<Parameters> Parameters { get; }

        public Mock<IExpanderDependencyManagerInteractor> IExpanderDependencyManagerInteractor { get; }

        internal Mock<IAssemblyContextInteractor> IAssemblyContextInteractor { get; }

        internal Mock<IObjectActivatorInteractor> IObjectActivatorInteractor { get; }

        internal Mock<IAssemblyManagerInteractor> IAssemblyManagerInteractor { get; }

        internal Mock<IGenericGateway<App>> IAppGateway { get; }

        internal Mock<IExpanderPluginLoaderInteractor> IExpanderPluginLoaderInteractor { get; }

        private App GetModel()
        {
            return new App();
        }
    }
}
