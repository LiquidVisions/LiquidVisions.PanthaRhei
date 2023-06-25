﻿using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Application.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Templates
{
    /// <summary>
    /// Loads static templates.
    /// </summary>
    internal class TemplateLoaderInteractor : ITemplateLoaderInteractor
    {
        private readonly IFile fileService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoaderInteractor"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
        public TemplateLoaderInteractor(IDependencyFactory dependencyFactory)
        {
            fileService = dependencyFactory.Get<IFile>();
            logger = dependencyFactory.Get<ILogger>();
        }

        /// <inheritdoc/>
        public string Load(string fullPathToTemplateFile)
        {
            if (fileService.Exists(fullPathToTemplateFile))
            {
                logger.Info($"Loading template on path '{fullPathToTemplateFile}'");

                return fileService.ReadAllText(fullPathToTemplateFile);
            }

            throw new TemplateException($"Failed to load template '{fullPathToTemplateFile}'");
        }
    }
}
