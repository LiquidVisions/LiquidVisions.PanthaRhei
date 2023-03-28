using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(x => new { x.Id });
            
            builder.Property(x => x.Id)
                .IsRequired(true);
            
            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);
            
            builder.Property(x => x.ReturnType)
                .IsRequired(false);
            
            builder.Property(x => x.IsCollection)
                .IsRequired(true);
            
            builder.Property(x => x.Modifier)
                .HasMaxLength(128)
                .IsRequired(true);
            
            builder.Property(x => x.GetModifier)
                .IsRequired(false);
            
            builder.Property(x => x.SetModifier)
                .IsRequired(false);
            
            builder.Property(x => x.Behaviour)
                .HasMaxLength(16)
                .IsRequired(false);
            
            builder.Property(x => x.Order)
                .IsRequired(true);
            
            builder.Property(x => x.Size)
                .IsRequired(false);
            
            builder.Property(x => x.Required)
                .IsRequired(true);
            
            builder.Property(x => x.IsKey)
                .IsRequired(true);
            
            builder.Property(x => x.IsIndex)
                .IsRequired(true);
            
            builder.HasOne(x => x.Reference)
                .WithMany(x => x.ReferencedIn)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Fields)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}