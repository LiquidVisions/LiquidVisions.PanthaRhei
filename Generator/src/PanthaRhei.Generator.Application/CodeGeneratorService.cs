using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.UseCases;

namespace LiquidVisions.PanthaRhei.Generator.Application
{
    /// <summary>
    /// An implemented type of the contract <seealso cref="ICodeGeneratorService"/>.
    /// </summary>
    internal class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly ICodeGeneratorBuilder builder;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorService"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/>.</param>
        public CodeGeneratorService(IDependencyResolver dependencyResolver)
        {
            builder = dependencyResolver.Get<ICodeGeneratorBuilder>();
            logger = dependencyResolver.Get<ILogger>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            try
            {
                ICodeGenerator codeGenerator = builder.Build();

                codeGenerator.Execute();
            }
            catch(CodeGenerationException ex)
            {
                logger.Fatal(ex, ex.Message);
            }
            catch(Exception ex)
            {
                logger.Fatal(ex, $"An unexpected error has occured with the following message: {ex.Message}.");
            }
        }
    }
}
