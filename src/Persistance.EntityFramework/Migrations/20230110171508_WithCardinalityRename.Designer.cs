﻿// <auto-generated />
using System;
using LiquidVisions.PanthaRhei.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230110171508_WithCardinalityRename")]
    partial class WithCardinalityRename
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.App", b =>
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Component", b =>
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.ConnectionString", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("ConnectionStrings");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Behaviour")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Callsite")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Modifier")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasDefaultValue("public");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasDefaultValue("class");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Expander", b =>
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Behaviour")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GetModifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCollection")
                        .HasColumnType("bit");

                    b.Property<bool>("IsIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Modifier")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasDefaultValue("public");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<Guid?>("ReferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Required")
                        .HasColumnType("bit");

                    b.Property<string>("ReturnType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetModifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("ReferenceId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Package", b =>
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

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Relationship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cardinality")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KeyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Required")
                        .HasColumnType("bit");

                    b.Property<string>("WithCardinality")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("WithForeignEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WithForeignEntityKeyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("KeyId");

                    b.HasIndex("WithForeignEntityId");

                    b.HasIndex("WithForeignEntityKeyId");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("AppExpander", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.App", null)
                        .WithMany()
                        .HasForeignKey("AppsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Expander", null)
                        .WithMany()
                        .HasForeignKey("ExpandersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Component", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Expander", "Expander")
                        .WithMany("Components")
                        .HasForeignKey("ExpanderId");

                    b.Navigation("Expander");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.ConnectionString", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.App", "App")
                        .WithMany("ConnectionStrings")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Entity", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.App", "App")
                        .WithMany("Entities")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Field", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Entity", "Entity")
                        .WithMany("Fields")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Entity", "Reference")
                        .WithMany("ReferencedIn")
                        .HasForeignKey("ReferenceId");

                    b.Navigation("Entity");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Package", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Component", "Component")
                        .WithMany("Packages")
                        .HasForeignKey("ComponentId");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Relationship", b =>
                {
                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Entity", "Entity")
                        .WithMany("Relations")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Field", "Key")
                        .WithMany("RelationshipKeys")
                        .HasForeignKey("KeyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Entity", "WithForeignEntity")
                        .WithMany("IsForeignEntityOf")
                        .HasForeignKey("WithForeignEntityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LiquidVisions.PanthaRhei.Domain.Entities.Field", "WithForeignEntityKey")
                        .WithMany("IsForeignEntityKeyOf")
                        .HasForeignKey("WithForeignEntityKeyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("Key");

                    b.Navigation("WithForeignEntity");

                    b.Navigation("WithForeignEntityKey");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.App", b =>
                {
                    b.Navigation("ConnectionStrings");

                    b.Navigation("Entities");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Component", b =>
                {
                    b.Navigation("Packages");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Entity", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("IsForeignEntityOf");

                    b.Navigation("ReferencedIn");

                    b.Navigation("Relations");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Expander", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("LiquidVisions.PanthaRhei.Domain.Entities.Field", b =>
                {
                    b.Navigation("IsForeignEntityKeyOf");

                    b.Navigation("RelationshipKeys");
                });
#pragma warning restore 612, 618
        }
    }
}