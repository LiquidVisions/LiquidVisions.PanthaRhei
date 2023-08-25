using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace __PREFIX____SOURCE__
{
    public class CreateProjectSolutionTask : IExpanderTask<__NAME__Expander>
    {
        private readonly __NAME__Expander expander;
        private readonly IDependencyFactory dependencyFactory;
        private readonly IProjectSolution solution;
        private readonly GenerationOptions options;

        public CreateProjectSolutionTask(__NAME__Expander expander, IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            solution = dependencyFactory.Get<IProjectSolution>();

            this.expander = expander;
            this.dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public string Name => nameof(CreateProjectSolutionTask);

        /// <inheritdoc/>
        public int Order => 1;

        /// <inheritdoc/>
        public __NAME__Expander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.Clean;

        /// <inheritdoc/>
        public void Execute()
        {
            solution.InitProjectSolution();
            solution.CreateComponentLibrary(expander.Model.Components.Single(x => x.Name == Resources.__NAME__), Resources.TemplateName);
        }
    }
}
