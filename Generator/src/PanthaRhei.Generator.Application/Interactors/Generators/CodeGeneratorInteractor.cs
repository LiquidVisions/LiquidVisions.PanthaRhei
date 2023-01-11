using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGeneratorInteractor"/>.
    /// </summary>
    internal sealed class CodeGeneratorInteractor : ICodeGeneratorInteractor
    {
        private readonly IEnumerable<IExpanderInteractor> expanders;
        private readonly Parameters parameters;
        private readonly IDirectory directory;

        public CodeGeneratorInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            parameters = dependencyFactory.Get<Parameters>();
            directory = dependencyFactory.Get<IDirectory>();
            expanders = dependencyFactory.GetAll<IExpanderInteractor>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (IExpanderInteractor expander in expanders.OrderBy(x => x.Model.Order))
            {
                expander.Harvest();

                Clean();

                expander.PreProcess();
                expander.Expand();
                expander.Rejuvenate();
                expander.PostProcess();
            }
        }

        private void Clean()
        {
            if (parameters.Clean)
            {
                directory.Delete(parameters.OutputFolder);
            }
        }
    }
}
