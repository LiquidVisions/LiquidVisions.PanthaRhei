using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Scriban;
using Scriban.Runtime;
using ITemplateLoader = LiquidVisions.PanthaRhei.Domain.Usecases.Templates.ITemplateLoader;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    /// <summary>
    /// <see cref="Template"/> implementation of <see cref="ITemplate"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ScribanTemplate"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
    internal class ScribanTemplate(IDependencyFactory dependencyFactory) : ITemplate
    {
        private readonly ILogger logger = dependencyFactory.Resolve<ILogger>();
        private readonly ITemplateLoader templateLoader = dependencyFactory.Resolve<Domain.Usecases.Templates.ITemplateLoader>();
        private readonly IFile fileService = dependencyFactory.Resolve<IFile>();
        private readonly IDirectory directoryService = dependencyFactory.Resolve<IDirectory>();
        private readonly ScriptObject scriptObject = dependencyFactory.Resolve<ScriptObject>();

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
