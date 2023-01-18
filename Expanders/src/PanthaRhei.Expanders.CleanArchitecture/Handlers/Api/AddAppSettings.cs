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
    public class AddAppSettings : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly IFile file;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAppSettings"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddAppSettings(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();
            file = dependencyFactory.Get<IFile>();
            this.expander = expander;
        }

        public int Order => 13;

        public string Name => nameof(AddAppSettings);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.GenerationMode.HasFlag(GenerationModes.Default)
            || parameters.GenerationMode.HasFlag(GenerationModes.Extend);

        /// <inheritdoc/>
        public void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Api);

            string folder = projectAgent.GetComponentOutputFolder(component);

            string path = System.IO.Path.Combine(folder, Resources.AppSettingsJson);
            string json = file.ReadAllText(path);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json);
            if (!jsonObject.ContainsKey("ConnectionStrings"))
            {
                JObject connectionStringObject = new();

                app.ConnectionStrings
                    .ToList()
                    .ForEach(x => connectionStringObject.Add(x.Name, x.Definition));

                jsonObject.Add("ConnectionStrings", connectionStringObject);

                string result = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                file.WriteAllText(path, result);
            }
        }
    }
}
