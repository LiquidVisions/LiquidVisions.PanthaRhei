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
    /// a <seealso cref="IExpanderTask{CleanArchitectureExpander}"/> that adds the request models to the output project.
    /// </summary>
    public class ExpandInteractorsTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly ITemplate templateService;
        private readonly App app;
        private readonly IDirectory directory;

        private readonly List<string> actions;
        private readonly Component component;
        private readonly string fullPathToComponentOutput;
        private readonly string destinationFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandInteractorsTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandInteractorsTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            templateService = dependencyFactory.Get<ITemplate>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries).ToList();
            component = expander.GetComponentByName(Resources.Application);
            fullPathToComponentOutput = expander.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(fullPathToComponentOutput, Resources.InteractorFolder);
        }

        /// <inheritdoc/>
        public int Order => 5;

        /// <inheritdoc/>
        public string Name => nameof(ExpandInteractorsTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                string endpointFolder = Path.Combine(destinationFolder, entity.Name.Pluralize());
                directory.Create(endpointFolder);

                foreach (string action in actions)
                {
                    string fullPathToTemplate = Expander.Model.GetPathToTemplate(options, $"{action}{Resources.InteractorTemplate}");
                    string fullPathToFile = Path.Combine(endpointFolder, $"{entity.ToFileName(action, "Interactor")}.cs");
                    object templateModel = new
                    {
                        component,
                        Entity = entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPathToFile);
                }
            }
        }
    }
}
