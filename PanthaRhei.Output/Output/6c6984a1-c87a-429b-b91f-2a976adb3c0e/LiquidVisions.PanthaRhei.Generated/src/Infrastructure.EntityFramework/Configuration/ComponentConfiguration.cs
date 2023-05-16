using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.HasKey(x => new { x.Id });
            
            builder.Property(x => x.Id)
                .IsRequired(true);
            
            builder.Property(x => x.Name)
                .HasMaxLength(32)
                .IsRequired(true);
            
            builder.Property(x => x.Description)
                .HasMaxLength(2056)
                .IsRequired(false);
            
            builder.HasOne(x => x.Expander)
                .WithMany(x => x.Components)
                .OnDelete(DeleteBehavior.NoAction);


            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}