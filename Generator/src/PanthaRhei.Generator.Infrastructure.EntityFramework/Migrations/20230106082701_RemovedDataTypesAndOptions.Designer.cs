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
    [Migration("20230106082701_RemovedDataTypesAndOptions")]
    partial class RemovedDataTypesAndOptions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);
            SqlServerModelBuilderExtensions.HasServiceTierSql(modelBuilder, "'Basic'");

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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Component", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(2056)
                        .HasColumnType("nvarchar(2056)");

                    b.Property<Guid?>("ExpanderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("ExpanderId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AppId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("TemplateFolder")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Expanders");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ReturnType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Handler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ExpanderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("SupportedGenerationModes")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Default");

                    b.HasKey("Id");

                    b.HasIndex("ExpanderId");

                    b.ToTable("Handlers");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("Packages");
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Component", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", "Expander")
                        .WithMany("Components")
                        .HasForeignKey("ExpanderId");

                    b.Navigation("Expander");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.App", null)
                        .WithMany("Entities")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Field", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", "Entity")
                        .WithMany("Fields")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Handler", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", "Expander")
                        .WithMany("Handlers")
                        .HasForeignKey("ExpanderId");

                    b.Navigation("Expander");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Package", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Component", "Component")
                        .WithMany("Packages")
                        .HasForeignKey("ComponentId");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.App", b =>
                {
                    b.Navigation("Entities");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Component", b =>
                {
                    b.Navigation("Packages");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", b =>
                {
                    b.Navigation("Components");

                    b.Navigation("Handlers");
                });
#pragma warning restore 612, 618
        }
    }
}
