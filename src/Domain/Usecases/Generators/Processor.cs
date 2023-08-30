using System;
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
        private readonly App _app;
        private readonly ICommandLine _commandLine;
        private readonly IDirectory _directoryService;
        private readonly ILogger _logger;
        private readonly GenerationOptions _options;
        private readonly TExpander _expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected Processor(IDependencyFactory dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory);

            _app = dependencyFactory.Resolve<App>();
            _commandLine = dependencyFactory.Resolve<ICommandLine>();
            _directoryService = dependencyFactory.Resolve<IDirectory>();
            _logger = dependencyFactory.Resolve<ILogger>();
            _options = dependencyFactory.Resolve<GenerationOptions>();
            _expander = dependencyFactory.Resolve<TExpander>();
        }

        /// <inheritdoc/>
        public App App => _app;

        /// <inheritdoc/>
        public TExpander Expander => _expander;

        /// <inheritdoc/>
        public abstract bool Enabled { get; }

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => _logger;

        /// <summary>
        /// Gets the <seealso cref="IDirectory"/>.
        /// </summary>
        public IDirectory DirectoryService => _directoryService;

        /// <summary>
        /// Gets the <seealso cref="ICommandLine"/>.
        /// </summary>
        public ICommandLine CommandLine => _commandLine;

        /// <summary>
        /// Gets the <seealso cref="Options"/>.
        /// </summary>
        public GenerationOptions Options => _options;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
