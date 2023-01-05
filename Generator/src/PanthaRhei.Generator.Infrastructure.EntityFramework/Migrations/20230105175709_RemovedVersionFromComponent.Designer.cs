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
    [Migration("20230105175709_RemovedVersionFromComponent")]
    partial class RemovedVersionFromComponent
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

            modelBuilder.Entity("EntityOptions", b =>
                {
                    b.Property<Guid>("EntitiesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OptionsKey")
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("EntitiesId", "OptionsId", "OptionsKey");

                    b.HasIndex("OptionsId", "OptionsKey");

                    b.ToTable("EntityOptions");
                });

            modelBuilder.Entity("FieldOptions", b =>
                {
                    b.Property<Guid>("FieldsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OptionsKey")
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("FieldsId", "OptionsId", "OptionsKey");

                    b.HasIndex("OptionsId", "OptionsKey");

                    b.ToTable("FieldOptions");
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.DataType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Name");

                    b.ToTable("DataTypes");

                    b.HasData(
                        new
                        {
                            Name = "string"
                        },
                        new
                        {
                            Name = "int"
                        },
                        new
                        {
                            Name = "decimal"
                        },
                        new
                        {
                            Name = "bool"
                        },
                        new
                        {
                            Name = "Guid"
                        },
                        new
                        {
                            Name = "DateTime"
                        },
                        new
                        {
                            Name = "Entity"
                        },
                        new
                        {
                            Name = "List"
                        });
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

                    b.Property<string>("DataTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("DataTypeName");

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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Option", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id", "Key");

                    b.ToTable("Options");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bc647b63-35ee-4527-867c-2114ab41cd8d"),
                            Key = "EntityType",
                            Value = "class"
                        },
                        new
                        {
                            Id = new Guid("60cbbc7a-4bf5-41be-aeda-e9bbfa2f2764"),
                            Key = "EntityType",
                            Value = "interface"
                        },
                        new
                        {
                            Id = new Guid("20c2975d-f693-4280-ba1f-fc60727c6911"),
                            Key = "EntityType",
                            Value = "enum"
                        },
                        new
                        {
                            Id = new Guid("931b2845-6d46-4ac0-8316-99c90511ad85"),
                            Key = "Keyword",
                            Value = "abstract"
                        },
                        new
                        {
                            Id = new Guid("a8945343-f1a0-465f-b201-5cac4a2ce64c"),
                            Key = "Keyword",
                            Value = "override"
                        });
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
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

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

            modelBuilder.Entity("EntityOptions", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", null)
                        .WithMany()
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Option", null)
                        .WithMany()
                        .HasForeignKey("OptionsId", "OptionsKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FieldOptions", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Option", null)
                        .WithMany()
                        .HasForeignKey("OptionsId", "OptionsKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Component", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Expander", null)
                        .WithMany("Components")
                        .HasForeignKey("ExpanderId");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.App", null)
                        .WithMany("Entities")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.Field", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.DataType", "DataType")
                        .WithMany("Fields")
                        .HasForeignKey("DataTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Generator.Domain.Models.Entity", "Entity")
                        .WithMany("Fields")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataType");

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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Generator.Domain.Models.DataType", b =>
                {
                    b.Navigation("Fields");
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
