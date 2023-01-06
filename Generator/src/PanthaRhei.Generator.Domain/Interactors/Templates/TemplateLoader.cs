using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates
{
    /// <summary>
    /// Loads static templates.
    /// </summary>
    internal class TemplateLoader : ITemplateLoader
    {
        private readonly IFile fileService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoader"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/>.</param>
        public TemplateLoader(IDependencyResolver dependencyResolver)
        {
            fileService = dependencyResolver.Get<IFile>();
            logger = dependencyResolver.Get<ILogger>();
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
