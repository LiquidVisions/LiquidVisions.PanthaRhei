using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers
{
    /// <summary>
    /// Adds the configured connectionstring as secrets the the generated project.
    /// </summary>
    public class AddConnectionStringAsSecretsTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly ICommandLine commandLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionStringAsSecretsTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public AddConnectionStringAsSecretsTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            commandLine = dependencyFactory.Get<ICommandLine>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
        }

        /// <inheritdoc/>
        public int Order => 16;

        /// <inheritdoc/>
        public string Name => nameof(AddConnectionStringAsSecretsTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.Modes.HasFlag(GenerationModes.Default)
            || options.Modes.HasFlag(GenerationModes.Extend);

        /// <inheritdoc/>
        public void Execute()
        {
            IEnumerable<string> paths = expander.GetComponentPaths(Resources.Api, Resources.EntityFramework);

            foreach (string path in paths)
            {
                commandLine.Start("dotnet user-secrets init", path);
                app.ConnectionStrings
                    .ToList()
                    .ForEach(x => commandLine.Start($"dotnet user-secrets set \"ConnectionStrings:{x.Name}\" \"{x.Definition}\"", path));
            }
        }
    }
}
