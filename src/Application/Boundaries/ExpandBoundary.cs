using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Boundaries
{
    /// <summary>
    /// An implemented type of the contract <seealso cref="IExpandBoundary"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ExpandBoundary"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
    internal class ExpandBoundary(IDependencyFactory dependencyFactory) : IExpandBoundary
    {
        private readonly GenerationOptions options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly ISeeder seeder = dependencyFactory.Resolve<ISeeder>();
        private readonly ICodeGeneratorBuilder builder = dependencyFactory.Resolve<ICodeGeneratorBuilder>();
        private readonly IMigrationService migrationService = dependencyFactory.Resolve<IMigrationService>();
        private readonly ILogger logger = dependencyFactory.Resolve<ILogger>();
        private readonly ILogger exceptionLogger = dependencyFactory
                .Resolve<ILogManager>()
                .GetExceptionLogger();

        /// <inheritdoc/>
        public void Execute()
        {
            logger.Info(options.ToString());

            TryMigrate();
            TrySeed();
            TryExpand();
        }

        private void TryMigrate()
        {
            if (options.Migrate)
            {
                migrationService.Migrate();
            }
        }

        private void TryExpand()
        {
            if (options.Modes != GenerationModes.None)
            {
                try
                {
                    ICodeGenerator generator = builder.Build();
                    generator.Execute();

                    logger.Info("Successfully completed the expanding process.");
                }
                catch (CodeGenerationException ex)
                {
                    logger.Fatal(ex, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    exceptionLogger.Fatal(ex, $"An unexpected error has occurred during the expanding processes with the following message: {ex.Message}.");
                    throw;
                }
            }
        }

        private void TrySeed()
        {
            if (seeder.Enabled)
            {
                try
                {
                    seeder.Execute();
                    logger.Info("Successfully completed the seeding generation process.");
                }
                catch (CodeGenerationException ex)
                {
                    logger.Fatal(ex, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    exceptionLogger.Fatal(ex, $"An unexpected error has occurred during the seeding processes with the following message: {ex.Message}.");
                    throw;
                }
            }
        }
    }
}
