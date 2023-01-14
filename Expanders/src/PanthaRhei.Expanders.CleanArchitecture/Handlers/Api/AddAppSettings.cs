using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Performs the required modifications to the appsettings.config.
    /// </summary>
    public class AddAppSettings : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAppSettings"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddAppSettings(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
        }

        public override int Order => 13;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Api);

            string folder = projectAgent.GetComponentOutputFolder(component);

            string path = System.IO.Path.Combine(folder, Resources.AppSettingsJson);
            string json = File.ReadAllText(path);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json);
            if (!jsonObject.ContainsKey("ConnectionStrings"))
            {
                JObject connectionStringObject = new();

                App.ConnectionStrings
                    .ToList()
                    .ForEach(x => connectionStringObject.Add(x.Name, x.Definition));

                jsonObject.Add("ConnectionStrings", connectionStringObject);

                string result = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                File.WriteAllText(path, result);
            }
        }
    }
}
