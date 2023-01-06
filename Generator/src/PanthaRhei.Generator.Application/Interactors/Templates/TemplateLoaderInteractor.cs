using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Templates
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
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/>.</param>
        public TemplateLoaderInteractor(IDependencyFactoryInteractor dependencyFactory)
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
