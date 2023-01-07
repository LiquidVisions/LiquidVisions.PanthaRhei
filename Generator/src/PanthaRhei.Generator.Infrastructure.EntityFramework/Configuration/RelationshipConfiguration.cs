using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Cardinality)
                .HasMaxLength(8)
                .IsRequired(true);

            builder.Property(x => x.WithyCardinality)
                .HasMaxLength(8)
                .IsRequired(true);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Relations)
                .IsRequired();

            builder.HasOne(x => x.Key)
                .WithMany(x => x.RelationshipKeys)
                .IsRequired();

            builder.HasOne(x => x.WithForeignEntity)
                .WithMany(x => x.IsForeignEntityOf)
                .IsRequired();

            builder.HasOne(x => x.WithForeignEntityKey)
                .WithMany(x => x.IsForeignEntityKeyOf)
                .IsRequired();
        }
    }
}