using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Performs the required modifications to the appsettings.config.
    /// </summary>
    public class ExpandAppSettingsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly IFile file;
        private readonly IWriterInteractor writer;
        private readonly Component component;
        private readonly App app;
        private readonly string fullPathToAppSettingsJson;
        private readonly string bootstrapFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandAppSettingsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandAppSettingsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            file = dependencyFactory.Get<IFile>();
            writer = dependencyFactory.Get<IWriterInteractor>();

            component = Expander.GetComponentByName(Resources.Api);
            string fullPathToApiComponent = expander.GetComponentOutputFolder(component);
            fullPathToAppSettingsJson = System.IO.Path.Combine(fullPathToApiComponent, Resources.AppSettingsJson);
            bootstrapFile = Path.Combine(fullPathToApiComponent, Resources.DependencyInjectionBootstrapperFile);
        }

        public int Order => 13;

        public string Name => nameof(ExpandAppSettingsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.Modes.HasFlag(GenerationModes.Default)
            || options.Modes.HasFlag(GenerationModes.Extend);

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
                    .ForEach(x => connectionStringObject.Add(x.Name, "CONNECTIONSTRING_IS_USER-SECRET"));

                jsonObject.Add("ConnectionStrings", connectionStringObject);

                string result = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                file.WriteAllText(fullPathToAppSettingsJson, result);
            }

            writer.Load(bootstrapFile);
            writer.Replace("CONNECTION_STRING_PLACEHOLDER", app.ConnectionStrings.Single().Name);
            writer.Save(bootstrapFile);
        }
    }
}
