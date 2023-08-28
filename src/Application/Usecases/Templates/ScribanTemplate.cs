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
    internal class ScribanTemplate : ITemplate
    {
        private readonly ILogger _logger;
        private readonly ITemplateLoader _templateLoader;
        private readonly IFile _fileService;
        private readonly IDirectory _directoryService;
        private readonly ScriptObject _scriptObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScribanTemplate"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
        public ScribanTemplate(IDependencyFactory dependencyFactory)
        {
            _logger = dependencyFactory.Get<ILogger>();
            _templateLoader = dependencyFactory.Get<Domain.Usecases.Templates.ITemplateLoader>();
            _fileService = dependencyFactory.Get<IFile>();
            _directoryService = dependencyFactory.Get<IDirectory>();
            _scriptObject = dependencyFactory.Get<ScriptObject>();
        }

        /// <inheritdoc/>
        public string Render(string fullTemplatePath, object model)
        {
            _scriptObject.Import(model);

            string template = _templateLoader.Load(fullTemplatePath);
            Template scribanTemplate = Template.Parse(template);

            TemplateContext context = new();
            context.PushGlobal(_scriptObject);
            string result = scribanTemplate.Render(context);
            context.PopGlobal();

            return result;
        }

        /// <inheritdoc/>
        public void RenderAndSave(string fullPathToTemplate, object templateModel, string fullPathToOutput)
        {
            string result = Render(fullPathToTemplate, templateModel);
            string folder = _fileService.GetDirectory(fullPathToOutput);

            if (!_directoryService.Exists(folder))
            {
                _directoryService.Create(folder);
                _logger.Trace($"Folder {folder} has been created.");
            }

            _logger.Trace($"Writing generated template to {fullPathToOutput}.");
            _fileService.WriteAllText(fullPathToOutput, result);
        }
    }
}
