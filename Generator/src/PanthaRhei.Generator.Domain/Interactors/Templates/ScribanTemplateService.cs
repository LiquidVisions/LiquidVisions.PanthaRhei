using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using Scriban;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates
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
            logger = dependencyResolver.Get<ILogger>();
            templateLoader = dependencyResolver.Get<ITemplateLoader>();
            fileService = dependencyResolver.Get<IFileService>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
        }

        /// <inheritdoc/>
        public string Render(string fullTemplatePath, object model)
        {
            ScriptObject scriptObject = new();
            scriptObject.Import(model);

            string template = templateLoader.Load(fullTemplatePath);
            Template scribanTemplate = Template.Parse(template);

            TemplateContext context = new();
            context.PushGlobal(scriptObject);
            string result = scribanTemplate.Render(context);
            context.PopGlobal();

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
