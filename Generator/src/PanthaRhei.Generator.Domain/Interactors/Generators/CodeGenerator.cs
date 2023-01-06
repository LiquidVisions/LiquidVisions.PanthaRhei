using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGenerator"/>.
    /// </summary>
    internal sealed class CodeGenerator : ICodeGenerator
    {
        private readonly IDependencyResolver dependencyResolver;
        private readonly Parameters parameters;
        private readonly IDirectoryService directory;

        public CodeGenerator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
            parameters = dependencyResolver.Get<Parameters>();
            directory = dependencyResolver.Get<IDirectoryService>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            IEnumerable<IExpander> expanders = dependencyResolver.GetAll<IExpander>();

            foreach (IExpander expander in expanders.OrderBy(x => x.Model.Order))
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
