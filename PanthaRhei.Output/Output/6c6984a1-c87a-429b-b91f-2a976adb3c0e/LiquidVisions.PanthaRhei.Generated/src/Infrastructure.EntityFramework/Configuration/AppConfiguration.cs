﻿using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Configuration
{
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.HasKey(x => new { x.Id });
            
            builder.Property(x => x.Id)
                .IsRequired(true);
            
            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);
            
            builder.Property(x => x.FullName)
                .HasMaxLength(2048)
                .IsRequired(true);
            

            #region ns-custom-configuration
            #endregion ns-custom-configuration
        }
    }
}