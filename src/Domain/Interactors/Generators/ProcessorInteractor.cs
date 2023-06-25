using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators
{
    /// <summary>
    /// An abstract implementation of the <see cref="IProcessorInteractor"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    public abstract class ProcessorInteractor<TExpander> : IProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly App app;
        private readonly ICommandLineInteractor commandLine;
        private readonly IDirectory directoryService;
        private readonly ILogger logger;
        private readonly GenerationOptions options;
        private readonly TExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected ProcessorInteractor(IDependencyFactory dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            commandLine = dependencyFactory.Get<ICommandLineInteractor>();
            directoryService = dependencyFactory.Get<IDirectory>();
            logger = dependencyFactory.Get<ILogger>();
            options = dependencyFactory.Get<GenerationOptions>();
            expander = dependencyFactory.Get<TExpander>();
        }

        /// <inheritdoc/>
        public App App => app;

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <inheritdoc/>
        public abstract bool CanExecute { get; }

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        /// <summary>
        /// Gets the <seealso cref="IDirectory"/>.
        /// </summary>
        public IDirectory DirectoryService => directoryService;

        /// <summary>
        /// Gets the <seealso cref="ICommandLineInteractor"/>.
        /// </summary>
        public ICommandLineInteractor CommandLine => commandLine;

        /// <summary>
        /// Gets the <seealso cref="Options"/>.
        /// </summary>
        public GenerationOptions Options => options;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
