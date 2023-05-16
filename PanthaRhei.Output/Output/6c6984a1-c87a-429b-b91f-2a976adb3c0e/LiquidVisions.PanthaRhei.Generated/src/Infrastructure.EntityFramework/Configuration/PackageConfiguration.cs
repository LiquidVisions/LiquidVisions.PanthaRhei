using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(x => new { x.Id });
            
            builder.Property(x => x.Id)
                .IsRequired(true);
            
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired(true);
            
            builder.Property(x => x.Version)
                .IsRequired(false);
            
            builder.HasOne(x => x.Component)
                .WithMany(x => x.Packages)
                .OnDelete(DeleteBehavior.NoAction);


            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}