using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.Handlers
{
    /// <summary>
    /// Abstract implementation of <seealso cref="IHandler{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">An instance of <see cref="IHandler{TExpander}"/>.</typeparam>
    public abstract class AbstractHandler<TExpander> : IHandler<TExpander>
        where TExpander : class, IExpander
    {
        private readonly App app;
        private readonly Parameters parameters;
        private readonly IDirectoryService directoryService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractHandler{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected AbstractHandler(IDependencyResolver dependencyResolver)
        {
            parameters = dependencyResolver.Get<Parameters>();
            app = dependencyResolver.Get<App>();
            fileService = dependencyResolver.Get<IFileService>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
            logger = dependencyResolver.Get<ILogger>();
            Expander = dependencyResolver.Get<TExpander>();
        }

        /// <inheritdoc/>
        public virtual string Name => GetType().Name;

        /// <summary>
        /// Gets the <seealso cref="Parameters"/>.
        /// </summary>
        public Parameters Parameters => parameters;

        /// <summary>
        /// Getst the <seealso cref="IDirectoryService"/>.
        /// </summary>
        public IDirectoryService DirectoryService => directoryService;

        /// <summary>
        /// Gets the <seealso cref="IFileService"/>.
        /// </summary>
        public IFileService FileService => fileService;

        /// <inheritdoc/>
        public TExpander Expander { get; }

        /// <inheritdoc/>
        public virtual bool CanExecute => Expander.Model
            .Handlers
            .Single(x => x.Name == Name)
            .SupportedGenerationModes
            .HasFlag(parameters.GenerationMode);

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        public Handler Model => throw new System.NotImplementedException();

        /// <summary>
        /// Gets the <seealso cref="App"/>.
        /// </summary>
        protected App App => app;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
