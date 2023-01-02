using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Moq;

namespace LiquidVisions.PanthaRhei.Generator.Tests
{
    public class Fakes
    {
        public Fakes()
        {
            IDependencyResolver = new();

            ILogger = new();
            IDependencyResolver
                .Setup(x => x.Get<ILogger>())
                .Returns(ILogger.Object);

            ICodeGenerator = new();
            IDependencyResolver
                .Setup(x => x.Get<ICodeGenerator>())
                .Returns(ICodeGenerator.Object);

            IDependencyManager = new();
            IDependencyResolver
                .Setup(x => x.Get<IDependencyManager>())
                .Returns(IDependencyManager.Object);

            ICodeGeneratorBuilder = new();
            ICodeGeneratorBuilder
                .Setup(x => x.Build())
                .Returns(ICodeGenerator.Object);
            IDependencyResolver
                .Setup(x => x.Get<ICodeGeneratorBuilder>())
                .Returns(ICodeGeneratorBuilder.Object);

            IFileService = new();
            IDependencyResolver
                .Setup(x => x.Get<IFileService>())
                .Returns(IFileService.Object);

            IDirectoryService = new();
            IDependencyResolver
                .Setup(x => x.Get<IDirectoryService>())
                .Returns(IDirectoryService.Object);

            Parameters = new()
            {
                Root = @"C:\Some\Root\Folder",
            };
            IDependencyResolver
                .Setup(x => x.Get<Parameters>())
                .Returns(Parameters);

            IAssemblyContext = new();
            IDependencyResolver
                .Setup(x => x.Get<IAssemblyContext>())
                .Returns(IAssemblyContext.Object);

            IObjectActivator = new();
            IDependencyResolver
                .Setup(x => x.Get<IObjectActivator>())
                .Returns(IObjectActivator.Object);

            IDependencyManager = new();
            IDependencyResolver
                .Setup(x => x.Get<IDependencyManager>())
                .Returns(IDependencyManager.Object);

            IExpanderDependencyManager = new();

            IAssemblyManager = new();
            IDependencyResolver
            .Setup(x => x.Get<IAssemblyManager>())
            .Returns(IAssemblyManager.Object);

            IAppRepository = new();
            IDependencyResolver
            .Setup(x => x.Get<IAppRepository>())
            .Returns(IAppRepository.Object);

            IExpanderPluginLoader = new();
            IDependencyResolver
            .Setup(x => x.Get<IExpanderPluginLoader>())
            .Returns(IExpanderPluginLoader.Object);
        }

        public Mock<IDependencyResolver> IDependencyResolver { get; }

        public Mock<IDependencyManager> IDependencyManager { get; }

        public Mock<ICodeGeneratorBuilder> ICodeGeneratorBuilder { get; }

        public Mock<ILogger> ILogger { get; }

        public Mock<ICodeGenerator> ICodeGenerator { get; }

        public Mock<IFileService> IFileService { get; }

        public Mock<IDirectoryService> IDirectoryService { get; }

        public Parameters Parameters { get; }

        public Mock<IExpanderDependencyManager> IExpanderDependencyManager { get; }

        internal Mock<IAssemblyContext> IAssemblyContext { get; }

        internal Mock<IObjectActivator> IObjectActivator { get; }

        internal Mock<IAssemblyManager> IAssemblyManager { get; }

        internal Mock<IAppRepository> IAppRepository { get; }

        internal Mock<IExpanderPluginLoader> IExpanderPluginLoader { get; }
    }
}
