using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(x => new { x.Id });
            
            builder.Property(x => x.Id)
                .IsRequired(true);
            
            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);
            
            builder.Property(x => x.Callsite)
                .HasMaxLength(2048)
                .IsRequired(true);
            
            builder.Property(x => x.Type)
                .HasMaxLength(16)
                .IsRequired(true);
            
            builder.Property(x => x.Modifier)
                .HasMaxLength(128)
                .IsRequired(true);
            
            builder.Property(x => x.Behaviour)
                .HasMaxLength(16)
                .IsRequired(false);
            
            builder.HasOne(x => x.App)
                .WithMany(x => x.Entities)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}