using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Scriban;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Templates
{
    /// <summary>
    /// <see cref="Template"/> implementation of <see cref="ITemplateService"/>.
    /// </summary>
    internal class ScribanTemplateService : ITemplateService
    {
        private readonly ILogger logger;
        private readonly ITemplateLoader templateLoader;
        private readonly IFileService fileService;
        private readonly IDirectoryService directoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScribanTemplateService"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/>.</param>
        public ScribanTemplateService(IDependencyResolver dependencyResolver)
        {
            this.logger = dependencyResolver.Get<ILogger>();
            this.templateLoader = dependencyResolver.Get<ITemplateLoader>();
            this.fileService = dependencyResolver.Get<IFileService>();
            this.directoryService = dependencyResolver.Get<IDirectoryService>();
        }

        /// <inheritdoc/>
        public string Render(string fullTemplatePath, object model)
        {
            string context = templateLoader.Load(fullTemplatePath);
            Template scribanTemplate = Template.Parse(context);
            string result = scribanTemplate.Render(model);

            return result;
        }

        /// <inheritdoc/>
        public void RenderAndSave(string fullPathToTemplate, object parameters, string fullPathToOutput)
        {
            string result = Render(fullPathToTemplate, parameters);
            string folder = fileService.GetDirectory(fullPathToOutput);

            if (!directoryService.Exists(folder))
            {
                directoryService.Create(folder);
                logger.Trace($"Folder {folder} has been created.");
            }

            logger.Trace($"Writing generated template to {fullPathToOutput}.");
            fileService.WriteAllText(fullPathToOutput, result);
        }
    }
}
