using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Performs the required modifications to the appsettings.config.
    /// </summary>
    public class ExpandAppSettingsTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly IFile file;
        private readonly IWriter writer;
        private readonly Component component;
        private readonly App app;
        private readonly string fullPathToAppSettingsJson;
        private readonly string bootstrapFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandAppSettingsTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandAppSettingsTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            file = dependencyFactory.Get<IFile>();
            writer = dependencyFactory.Get<IWriter>();

            component = Expander.GetComponentByName(Resources.Api);
            string fullPathToApiComponent = expander.GetComponentOutputFolder(component);
            fullPathToAppSettingsJson = System.IO.Path.Combine(fullPathToApiComponent, Resources.AppSettingsJson);
            bootstrapFile = Path.Combine(fullPathToApiComponent, Resources.DependencyInjectionBootstrapperFile);
        }

        public int Order => 13;

        public string Name => nameof(ExpandAppSettingsTask);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.Modes.HasFlag(GenerationModes.Default)
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
