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
    internal class ExpandBoundary : IExpandBoundary
    {
        private readonly ILogger logger;
        private readonly ILogger exceptionLogger;
        private readonly GenerationOptions options;
        private readonly ISeeder seeder;
        private readonly ICodeGeneratorBuilder builder;
        private readonly IMigrationService migrationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandBoundary"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
        public ExpandBoundary(IDependencyFactory dependencyFactory)
        {
            seeder = dependencyFactory.Get<ISeeder>();
            builder = dependencyFactory.Get<ICodeGeneratorBuilder>();
            logger = dependencyFactory.Get<ILogger>();
            migrationService = dependencyFactory.Get<IMigrationService>();
            options = dependencyFactory.Get<GenerationOptions>();

            exceptionLogger = dependencyFactory
                .Get<ILogManager>()
                .GetExceptionLogger();
        }

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
                }
                catch (Exception ex)
                {
                    exceptionLogger.Fatal(ex, $"An unexpected error has occured during the expanding procecess with the following message: {ex.Message}.");
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
                }
                catch (Exception ex)
                {
                    exceptionLogger.Fatal(ex, $"An unexpected error has occured during the seeding processes with the following message: {ex.Message}.");
                }
            }
        }
    }
}
