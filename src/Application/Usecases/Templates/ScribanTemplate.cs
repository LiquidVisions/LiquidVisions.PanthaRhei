using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Scriban;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    /// <summary>
    /// <see cref="Template"/> implementation of <see cref="ITemplate"/>.
    /// </summary>
    internal class ScribanTemplate : ITemplate
    {
        private readonly ILogger logger;
        private readonly Domain.Usecases.Templates.ITemplateLoader templateLoader;
        private readonly IFile fileService;
        private readonly IDirectory directoryService;
        private readonly ScriptObject scriptObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScribanTemplate"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
        public ScribanTemplate(IDependencyFactory dependencyFactory)
        {
            logger = dependencyFactory.Get<ILogger>();
            templateLoader = dependencyFactory.Get<Domain.Usecases.Templates.ITemplateLoader>();
            fileService = dependencyFactory.Get<IFile>();
            directoryService = dependencyFactory.Get<IDirectory>();
            scriptObject = dependencyFactory.Get<ScriptObject>();
        }

        /// <inheritdoc/>
        public string Render(string fullTemplatePath, object model)
        {
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
        public void RenderAndSave(string fullPathToTemplate, object templateModel, string fullPathToOutput)
        {
            string result = Render(fullPathToTemplate, templateModel);
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
