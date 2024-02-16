using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    /// <summary>
    /// Loads static templates.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TemplateLoader"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
    internal class TemplateLoader(IDependencyFactory dependencyFactory) : ITemplateLoader
    {
        private readonly IFile _fileService = dependencyFactory.Resolve<IFile>();
        private readonly ILogger _logger = dependencyFactory.Resolve<ILogger>();

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
