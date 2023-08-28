using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.Application.Tests
{
    /// <summary>
    /// The <see cref="Fakes"/> for the <see cref="Application"/> project.
    /// </summary>
    public class ApplicationFakes : Fakes
    {
        /// <summary>
        /// Gets a mocked instance of <seealso cref="IExpanderPluginLoader"/>.
        /// </summary>
        public Mock<IExpanderPluginLoader> IExpanderPluginLoader { get; } = new();

        /// <summary>
        /// Gets a mocked instance of <seealso cref="ICodeGeneratorBuilder"/>.
        /// </summary>
        public Mock<ICodeGeneratorBuilder> ICodeGeneratorBuilder { get; } = new();

        /// <summary>
        /// Gets a mocked instance of <seealso cref="IMigrationService"/>.
        /// </summary>
        public Mock<IMigrationService> IMigrationService { get; } = new();

        /// <summary>
        /// Gets a mocked instance of <seealso cref="ICodeGenerator"/>.
        /// </summary>
        public Mock<ICodeGenerator> ICodeGenerator { get; } = new();

        /// <summary>
        /// Gets a mocked instance of <seealso cref="IAssemblyContext"/>.
        /// </summary>
        internal Mock<IAssemblyContext> IAssemblyContext { get; } = new();

        /// <summary>
        /// Gets a mocked instance of <seealso cref="IObjectActivator"/>.
        /// </summary>
        internal Mock<IObjectActivator> IObjectActivator { get; } = new();

        /// <summary>
        /// Configures the mocks globally.
        /// </summary>
        public override void Configure()
        {
            base.Configure();

            ICodeGeneratorBuilder.Setup(x => x.Build()).Returns(ICodeGenerator.Object);
        }

        /// <summary>
        /// Configures the mocks for the <seealso cref="IDependencyFactory"/>.
        /// </summary>
        public override void ConfigureIDependencyFactory()
        {
            base.ConfigureIDependencyFactory();

            IDependencyFactory.Setup(x => x.Get<IAssemblyContext>()).Returns(IAssemblyContext.Object);
            IDependencyFactory.Setup(x => x.Get<IObjectActivator>()).Returns(IObjectActivator.Object);
            IDependencyFactory.Setup(x => x.Get<ICodeGenerator>()).Returns(ICodeGenerator.Object);
            IDependencyFactory.Setup(x => x.Get<ICodeGeneratorBuilder>()).Returns(ICodeGeneratorBuilder.Object);
            IDependencyFactory.Setup(x => x.Get<IExpanderPluginLoader>()).Returns(IExpanderPluginLoader.Object);
            IDependencyFactory.Setup(x => x.Get<IMigrationService>()).Returns(IMigrationService.Object);
        }
    }
}
