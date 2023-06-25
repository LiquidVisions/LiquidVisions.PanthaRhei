using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates validator classes.
    /// </summary>
    public class ExpandValidatorsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly IDirectory directory;

        private readonly List<string> actions;
        private readonly Component component;
        private readonly string fullPathToComponentOutput;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandValidatorsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandValidatorsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries).ToList();
            component = expander.GetComponentByName(Resources.Application);
            fullPathToComponentOutput = expander.GetComponentOutputFolder(component);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ValidatorTemplate);
        }

        public int Order => 6;

        public string Name => nameof(ExpandValidatorsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                string fullpathToDestinationFolder = Path.Combine(fullPathToComponentOutput, Resources.ValidatorFolder, entity.Name.Pluralize());
                directory.Create(fullpathToDestinationFolder);

                foreach (string action in actions)
                {
                    string filePath = Path.Combine(fullpathToDestinationFolder, $"{entity.ToFileName(action, "Validator")}.cs");
                    object templateModel = new
                    {
                        component,
                        Action = action,
                        Entity = entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, filePath);
                }
            }
        }
    }
}
