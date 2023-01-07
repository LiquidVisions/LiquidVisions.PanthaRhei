using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework
{
    /// <summary>
    /// Represents an implementation of <seealso cref="DbContext"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Context : DbContext
    {
        private readonly IDependencyManagerInteractor dependencyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="options"><seealso cref="DbContextOptions"/></param>
        public Context(DbContextOptions<Context> options, IDependencyFactoryInteractor factory)
            : base(options)
        {
            dependencyManager = factory.Get<IDependencyManagerInteractor>();
        }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{App}"/>.
        /// </summary>
        public DbSet<App> Apps { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{Expander}"/>.
        /// </summary>
        public DbSet<Expander> Expanders { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{Component}"/>.
        /// </summary>
        public DbSet<Component> Components { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{Package}"/>.
        /// </summary>
        public DbSet<Package> Packages { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{Entity}"/>.
        /// </summary>
        public DbSet<Entity> Entities { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{Field}"/>.
        /// </summary>
        public DbSet<Field> Fields { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{ConnectionStrings}"/>.
        /// </summary>
        public DbSet<ConnectionString> ConnectionStrings { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: make this more secure...
                optionsBuilder
                    .UseLoggerFactory(ContextExtensions.GetLoggerFactory())
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(@"Server=tcp:liquidvisions.database.windows.net,1433;Initial Catalog=PantaRhei.Dev;Persist Security Info=False;User ID=gerco.koks;Password=4cZ#Lsojpc75;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }



        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly)
                .HasServiceTier("Basic");

            dependencyManager.AddSingleton(modelBuilder);
            dependencyManager.Build();
        }
    }
}
