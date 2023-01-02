using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGenerator"/>.
    /// </summary>
    internal sealed class CodeGenerator : ICodeGenerator
    {
        private readonly IDependencyResolver dependencyResolver;
        private readonly Parameters parameters;
        private readonly IDirectoryService directory;
        private readonly ILogger logger;

        public CodeGenerator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
            this.parameters = dependencyResolver.Get<Parameters>();
            this.directory = dependencyResolver.Get<IDirectoryService>();
            this.logger = dependencyResolver.Get<ILogger>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            IEnumerable<IExpander> expanders = dependencyResolver.GetAll<IExpander>();

            foreach(IExpander expander in expanders.OrderBy(x => x.Model.Order))
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
            if(parameters.Clean)
            {
                directory.Delete(parameters.OutputFolder);
            }
        }
    }
}
