using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
    public class ExpanderConfiguration : IEntityTypeConfiguration<Expander>
    {
        public void Configure(EntityTypeBuilder<Expander> builder)
        {
            builder.HasKey(x => new { x.Id });
            
            builder.Property(x => x.Id)
                .IsRequired(true);
            
            builder.Property(x => x.Name)
                .IsRequired(false);
            
            builder.Property(x => x.TemplateFolder)
                .IsRequired(false);
            
            builder.Property(x => x.Order)
                .IsRequired(true);
            

            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}