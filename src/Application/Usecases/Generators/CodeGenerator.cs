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
        private readonly IEnumerable<IExpander> _expanders;
        private readonly GenerationOptions _options;
        private readonly IDirectory _directory;

        public CodeGenerator(IDependencyFactory dependencyFactory)
        {
            _options = dependencyFactory.Get<GenerationOptions>();
            _directory = dependencyFactory.Get<IDirectory>();
            _expanders = dependencyFactory.GetAll<IExpander>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (IExpander expander in _expanders.OrderBy(x => x.Model.Order))
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
            if (_options.Clean)
            {
                expander.Clean();

                _directory.Delete(_options.OutputFolder);
            }
        }
    }
}
