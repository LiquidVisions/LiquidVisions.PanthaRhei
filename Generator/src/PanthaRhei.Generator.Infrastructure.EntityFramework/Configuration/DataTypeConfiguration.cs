using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class DataTypeConfiguration : IEntityTypeConfiguration<DataType>
    {
        public void Configure(EntityTypeBuilder<DataType> builder)
        {
            builder.HasKey(x => new { x.Name });

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(32);

            builder.HasData(new[]
            {
                new DataType { Name = "string" },
                new DataType { Name = "int" },
                new DataType { Name = "decimal" },
                new DataType { Name = "bool" },
                new DataType { Name = "Guid" },
                new DataType { Name = "DateTime" },
                new DataType { Name = "Entity" },
                new DataType { Name = "List" },
            });
        }
    }
}
