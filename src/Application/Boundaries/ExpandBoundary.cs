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
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly ISeeder _seeder = dependencyFactory.Resolve<ISeeder>();
        private readonly ICodeGeneratorBuilder _builder = dependencyFactory.Resolve<ICodeGeneratorBuilder>();
        private readonly IMigrationService _migrationService = dependencyFactory.Resolve<IMigrationService>();
        private readonly ILogger _logger = dependencyFactory.Resolve<ILogger>();
        private readonly ILogger _exceptionLogger = dependencyFactory
                .Resolve<ILogManager>()
                .GetExceptionLogger();

        /// <inheritdoc/>
        public void Execute()
        {
            _logger.Info(_options.ToString());

            TryMigrate();
            TrySeed();
            TryExpand();
        }

        private void TryMigrate()
        {
            if (_options.Migrate)
            {
                _migrationService.Migrate();
            }
        }

        private void TryExpand()
        {
            if (_options.Modes != GenerationModes.None)
            {
                try
                {
                    ICodeGenerator generator = _builder.Build();
                    generator.Execute();

                    _logger.Info("Successfully completed the expanding process.");
                }
                catch (CodeGenerationException ex)
                {
                    _logger.Fatal(ex, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    _exceptionLogger.Fatal(ex, $"An unexpected error has occurred during the expanding processes with the following message: {ex.Message}.");
                    throw;
                }
            }
        }

        private void TrySeed()
        {
            if (_seeder.Enabled)
            {
                try
                {
                    _seeder.Execute();
                    _logger.Info("Successfully completed the seeding generation process.");
                }
                catch (CodeGenerationException ex)
                {
                    _logger.Fatal(ex, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    _exceptionLogger.Fatal(ex, $"An unexpected error has occurred during the seeding processes with the following message: {ex.Message}.");
                    throw;
                }
            }
        }
    }
}
