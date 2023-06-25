using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers
{
    public class ExpandSecretsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly ICommandLineInteractor commandLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandSecretsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandSecretsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            commandLine = dependencyFactory.Get<ICommandLineInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
        }

        public int Order => 16;

        public string Name => nameof(ExpandSecretsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

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
