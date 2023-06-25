using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGenerator"/>.
    /// </summary>
    internal sealed class CodeGenerator : ICodeGenerator
    {
        private readonly IEnumerable<IExpander> expanders;
        private readonly GenerationOptions options;
        private readonly IDirectory directory;

        public CodeGenerator(IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            directory = dependencyFactory.Get<IDirectory>();
            expanders = dependencyFactory.GetAll<IExpander>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (IExpander expander in expanders.OrderBy(x => x.Model.Order))
            {
                expander.Harvest();

                Clean(expander);

                expander.PreProcess();
                expander.Expand();
                expander.Rejuvenate();
                expander.PostProcess();
            }
        }

        private void Clean(IExpander expander)
        {
            if (options.Clean)
            {
                expander.Clean();

                directory.Delete(options.OutputFolder);
            }
        }
    }
}
