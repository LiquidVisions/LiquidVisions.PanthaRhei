using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ConnectionStringConfiguration : IEntityTypeConfiguration<ConnectionString>
    {
        public void Configure(EntityTypeBuilder<ConnectionString> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.Definition)
                .HasMaxLength(2048)
                .IsRequired(true);

            builder.HasOne(x => x.App)
                .WithMany(x => x.ConnectionStrings)
                .IsRequired();
        }
    }
}