using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework
{
    /// <summary>
    /// Represents an implementation of <seealso cref="DbContext"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Context : DbContext
    {
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
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
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

        /// <summary>
        /// Gets or sets the <seealso cref="DbSet{Relationships}"/>.
        /// </summary>
        public DbSet<Relationship> Relationships { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .AddUserSecrets<Context>();
                var configurationRoot = builder.Build();

                string connectionString = configurationRoot.GetConnectionString("PanthaRheiDev");

                optionsBuilder
                    .UseLoggerFactory(ContextExtensions.GetLoggerFactory())
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(connectionString);
            }
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly)
                .HasServiceTier("Basic");
        }
    }
}
