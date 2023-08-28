using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    /// <summary>
    /// Loads static templates.
    /// </summary>
    internal class TemplateLoader : ITemplateLoader
    {
        private readonly IFile _fileService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoader"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
        public TemplateLoader(IDependencyFactory dependencyFactory)
        {
            _fileService = dependencyFactory.Get<IFile>();
            _logger = dependencyFactory.Get<ILogger>();
        }

        /// <inheritdoc/>
        public string Load(string fullPathToTemplateFile)
        {
            if (_fileService.Exists(fullPathToTemplateFile))
            {
                _logger.Info($"Loading template on path '{fullPathToTemplateFile}'");

                return _fileService.ReadAllText(fullPathToTemplateFile);
            }

            throw new TemplateException($"Failed to load template '{fullPathToTemplateFile}'");
        }
    }
}
