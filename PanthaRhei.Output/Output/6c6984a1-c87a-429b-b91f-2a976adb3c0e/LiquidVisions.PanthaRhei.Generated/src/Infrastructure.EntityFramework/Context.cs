﻿using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions options)
            : base(options) { }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddConsole();
        });

        public DbSet<Field> Fields { get; set; }
        
        public DbSet<App> Apps { get; set; }
        
        public DbSet<Package> Packages { get; set; }
        
        public DbSet<Entity> Entities { get; set; }
        
        public DbSet<Component> Components { get; set; }
        
        public DbSet<Expander> Expanders { get; set; }
        
        public DbSet<ConnectionString> ConnectionStrings { get; set; }
        
        public DbSet<Relationship> Relationships { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(@"PUT_YOUR_CONNECTIONSTRING_HERE");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}