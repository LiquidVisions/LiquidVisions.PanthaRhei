﻿// <auto-generated />
using System;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20221231225655_addedAppExpanderManyToMany")]
    partial class addedAppExpanderManyToMany
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppExpander", b =>
                {
                    b.Property<Guid>("AppsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExpandersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppsId", "ExpandersId");

                    b.HasIndex("ExpandersId");

                    b.ToTable("AppExpander");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.App", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Expander");
                });

            modelBuilder.Entity("AppExpander", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.App", null)
                        .WithMany()
                        .HasForeignKey("AppsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", null)
                        .WithMany()
                        .HasForeignKey("ExpandersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
