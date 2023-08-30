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
        private readonly ILogger _logger;
        private readonly ILogger _exceptionLogger;
        private readonly GenerationOptions _options;
        private readonly ISeeder _seeder;
        private readonly ICodeGeneratorBuilder _builder;
        private readonly IMigrationService _migrationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandBoundary"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/>.</param>
        public ExpandBoundary(IDependencyFactory dependencyFactory)
        {
            _seeder = dependencyFactory.Resolve<ISeeder>();
            _builder = dependencyFactory.Resolve<ICodeGeneratorBuilder>();
            _logger = dependencyFactory.Resolve<ILogger>();
            _migrationService = dependencyFactory.Resolve<IMigrationService>();
            _options = dependencyFactory.Resolve<GenerationOptions>();

            _exceptionLogger = dependencyFactory
                .Resolve<ILogManager>()
                .GetExceptionLogger();
        }

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
                    _exceptionLogger.Fatal(ex, $"An unexpected error has occured during the expanding procecess with the following message: {ex.Message}.");
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
                    _exceptionLogger.Fatal(ex, $"An unexpected error has occured during the seeding processes with the following message: {ex.Message}.");
                    throw;
                }
            }
        }
    }
}
