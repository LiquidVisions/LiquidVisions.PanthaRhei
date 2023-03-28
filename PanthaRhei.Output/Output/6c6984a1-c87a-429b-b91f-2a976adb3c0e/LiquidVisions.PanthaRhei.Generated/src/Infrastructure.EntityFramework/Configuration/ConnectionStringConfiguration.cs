using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
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
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}