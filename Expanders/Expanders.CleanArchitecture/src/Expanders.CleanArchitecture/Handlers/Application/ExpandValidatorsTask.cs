using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates validator classes.
    /// </summary>
    public class ExpandValidatorsTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly ITemplate templateService;
        private readonly App app;
        private readonly IDirectory directory;

        private readonly List<string> actions;
        private readonly Component component;
        private readonly string fullPathToComponentOutput;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandValidatorsTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandValidatorsTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Resolve<GenerationOptions>();
            templateService = dependencyFactory.Resolve<ITemplate>();
            app = dependencyFactory.Resolve<App>();
            directory = dependencyFactory.Resolve<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries).ToList();
            component = expander.GetComponentByName(Resources.Application);
            fullPathToComponentOutput = expander.GetComponentOutputFolder(component);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ValidatorTemplate);
        }

        /// <inheritdoc/>
        public int Order => 6;

        /// <inheritdoc/>
        public string Name => nameof(ExpandValidatorsTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
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
