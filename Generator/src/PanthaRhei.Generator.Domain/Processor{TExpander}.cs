using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain
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
        private readonly IDirectoryService directoryService;
        private readonly ILogger logger;
        private readonly Parameters parameters;
        private readonly TExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected Processor(IDependencyResolver dependencyResolver)
        {
            app = dependencyResolver.Get<App>();
            commandLine = dependencyResolver.Get<ICommandLine>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
            logger = dependencyResolver.Get<ILogger>();
            parameters = dependencyResolver.Get<Parameters>();
            expander = dependencyResolver.Get<TExpander>();
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
        /// Gets the <seealso cref="IDirectoryService"/>.
        /// </summary>
        public IDirectoryService DirectoryService => directoryService;

        /// <summary>
        /// Gets the <seealso cref="ICommandLine"/>.
        /// </summary>
        public ICommandLine CommandLine => commandLine;

        /// <summary>
        /// Gets the <seealso cref="Parameters"/>.
        /// </summary>
        public Parameters Parameters => parameters;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
