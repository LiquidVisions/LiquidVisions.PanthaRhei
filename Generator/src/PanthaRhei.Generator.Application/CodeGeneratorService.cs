using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Application
{
    /// <summary>
    /// An implemented type of the contract <seealso cref="ICodeGeneratorService"/>.
    /// </summary>
    internal class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly ICodeGeneratorBuilderInteractor builder;
        private readonly ILogger logger;
        private readonly ILogger exceptionLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorService"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/>.</param>
        public CodeGeneratorService(IDependencyFactoryInteractor dependencyFactory)
        {
            builder = dependencyFactory.Get<ICodeGeneratorBuilderInteractor>();
            logger = dependencyFactory.Get<ILogger>();
            exceptionLogger = dependencyFactory
                .Get<ILogManager>()
                .GetExceptionLogger();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            try
            {
                ICodeGeneratorInteractor codeGenerator = builder.Build();

                codeGenerator.Execute();
            }
            catch (CodeGenerationException ex)
            {
                logger.Fatal(ex, ex.Message);
            }
            catch (Exception ex)
            {
                exceptionLogger.Fatal(ex, $"An unexpected error has occured with the following message: {ex.Message}.");
            }

            logger.Info("Successfully completed the code generation process.");
        }
    }
}
