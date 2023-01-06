﻿using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers
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
        private readonly TExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractHandler{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"><typeparamref name="TExpander"/>.</param>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected AbstractHandler(TExpander expander, IDependencyResolver dependencyResolver)
        {
            this.expander = expander;

            parameters = dependencyResolver.Get<Parameters>();
            app = dependencyResolver.Get<App>();
            fileService = dependencyResolver.Get<IFileService>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
            logger = dependencyResolver.Get<ILogger>();
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

        public abstract int Order { get; }

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <inheritdoc/>
        public abstract bool CanExecute { get; }

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        /// <summary>
        /// Gets the <seealso cref="App"/>.
        /// </summary>
        protected App App => app;

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
