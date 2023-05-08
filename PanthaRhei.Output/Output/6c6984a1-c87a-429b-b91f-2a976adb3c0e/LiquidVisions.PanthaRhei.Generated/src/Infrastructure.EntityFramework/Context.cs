using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(@"Server=tcp:liquidvisions.database.windows.net,1433;Initial Catalog=PantaRhei.Prod;Persist Security Info=False;User ID=gerco.koks;Password=1qJ4AFcHyb7QL4gM!5n2vk2@^$%U;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}