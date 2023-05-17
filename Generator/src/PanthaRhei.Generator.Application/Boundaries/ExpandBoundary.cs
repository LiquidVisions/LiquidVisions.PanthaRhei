using System;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Application.Boundaries
{
    /// <summary>
    /// An implemented type of the contract <seealso cref="IExpandBoundary"/>.
    /// </summary>
    internal class ExpandBoundary : IExpandBoundary
    {
        private readonly ICodeGeneratorBuilderInteractor builder;
        private readonly ISeederInteractor seederInteractor;
        private readonly ILogger logger;
        private readonly ILogger exceptionLogger;
        private readonly ExpandRequestModel requestModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandBoundary"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/>.</param>
        public ExpandBoundary(IDependencyFactoryInteractor dependencyFactory)
        {
            builder = dependencyFactory.Get<ICodeGeneratorBuilderInteractor>();
            seederInteractor = dependencyFactory.Get<ISeederInteractor>();
            logger = dependencyFactory.Get<ILogger>();
            exceptionLogger = dependencyFactory
                .Get<ILogManager>()
                .GetExceptionLogger();

            requestModel = dependencyFactory.Get<ExpandRequestModel>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            logger.Info(requestModel.ToString());

            if(!TrySeed())
            {
                TryExpand();
            }
        }

        private void TryExpand()
        {
            try
            {
                builder.Build()
                    .Execute();

                logger.Info("Successfully completed the expanding process.");
            }
            catch (CodeGenerationException ex)
            {
                logger.Fatal(ex, ex.Message);
            }
            catch (Exception ex)
            {
                exceptionLogger.Fatal(ex, $"An unexpected error has occured during the expanding procecess with the following message: {ex.Message}.");
            }
        }

        private bool TrySeed()
        {
            try
            {
                if (seederInteractor.CanExecute)
                {
                    seederInteractor.Execute();
                    logger.Info("Successfully completed the seeding generation process.");

                    return true;
                }
            }
            catch (CodeGenerationException ex)
            {
                logger.Fatal(ex, ex.Message);
            }
            catch (Exception ex)
            {
                exceptionLogger.Fatal(ex, $"An unexpected error has occured during the seeding processes with the following message: {ex.Message}.");
            }

            return false;
        }
    }
}
