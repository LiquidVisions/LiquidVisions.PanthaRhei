using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Performs the required modifications to the appsettings.config.
    /// </summary>
    public class ExpandAppSettingsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly IFile file;
        private readonly Component component;
        private readonly App app;
        private readonly string fullPathToAppSettingsJson;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandAppSettingsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandAppSettingsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            file = dependencyFactory.Get<IFile>();

            component = Expander.Model.GetComponentByName(Resources.Api);
            string fullPathToApiComponent = projectAgent.GetComponentOutputFolder(component);
            fullPathToAppSettingsJson = System.IO.Path.Combine(fullPathToApiComponent, Resources.AppSettingsJson);
        }

        public int Order => 13;

        public string Name => nameof(ExpandAppSettingsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.GenerationMode.HasFlag(GenerationModes.Default)
            || options.GenerationMode.HasFlag(GenerationModes.Extend);

        /// <inheritdoc/>
        public void Execute()
        {
            string jsonFile = file.ReadAllText(fullPathToAppSettingsJson);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonFile);

            if (!jsonObject.ContainsKey("ConnectionStrings"))
            {
                JObject connectionStringObject = new();

                app.ConnectionStrings
                    .ToList()
                    .ForEach(x => connectionStringObject.Add(x.Name, x.Definition));

                jsonObject.Add("ConnectionStrings", connectionStringObject);

                string result = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                file.WriteAllText(fullPathToAppSettingsJson, result);
            }
        }
    }
}
