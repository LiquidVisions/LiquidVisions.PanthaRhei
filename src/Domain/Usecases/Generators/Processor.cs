using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators
{
    /// <summary>
    /// An abstract implementation of the <see cref="IProcessor"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class Processor<TExpander> : IProcessor<TExpander>
        where TExpander : class, IExpander
    {
        private readonly App app;
        private readonly ICommandLine commandLine;
        private readonly IDirectory directoryService;
        private readonly ILogger logger;
        private readonly GenerationOptions options;
        private readonly TExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected Processor(IDependencyFactory dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            commandLine = dependencyFactory.Get<ICommandLine>();
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
        public abstract bool Enabled { get; }

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        /// <summary>
        /// Gets the <seealso cref="IDirectory"/>.
        /// </summary>
        public IDirectory DirectoryService => directoryService;

        /// <summary>
        /// Gets the <seealso cref="ICommandLine"/>.
        /// </summary>
        public ICommandLine CommandLine => commandLine;

        /// <summary>
        /// Gets the <seealso cref="Options"/>.
        /// </summary>
        public GenerationOptions Options => options;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
