﻿// <auto-generated />
using System;
using ClickAndEatApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClickAndEatApi.Db.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230514132101_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClickAndEatApi.Db.Models.FoodTypeEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Identifier");

                    b.HasIndex("OrganizationEntityId");

                    b.HasIndex("Type", "OrganizationEntityId")
                        .IsUnique();

                    b.ToTable("FoodTypeEntities");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.MenuEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderLockState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ShoppingLimit")
                        .HasColumnType("int");

                    b.HasKey("Identifier");

                    b.HasIndex("OrganizationEntityId")
                        .IsUnique();

                    b.ToTable("MenuEntities");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.OrderEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderDeliverState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identifier");

                    b.HasIndex("OrganizationEntityId");

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("OrderEntities");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.OrganizationEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identifier");

                    b.ToTable("OrganizationEntities");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.ShoppingCartEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identifier");

                    b.HasIndex("OrganizationEntityId");

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("ShoppingCartEntities");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.UserEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identifier");

                    b.HasIndex("OrganizationEntityId");

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("FoodTypeEntityMenuEntity", b =>
                {
                    b.Property<Guid>("FoodTypeEntitiesIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MenuEntitiesIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FoodTypeEntitiesIdentifier", "MenuEntitiesIdentifier");

                    b.HasIndex("MenuEntitiesIdentifier");

                    b.ToTable("FoodTypeEntityMenuEntity");
                });

            modelBuilder.Entity("FoodTypeEntityOrderEntity", b =>
                {
                    b.Property<Guid>("FoodTypeEntitiesIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderEntitiesIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FoodTypeEntitiesIdentifier", "OrderEntitiesIdentifier");

                    b.HasIndex("OrderEntitiesIdentifier");

                    b.ToTable("FoodTypeEntityOrderEntity");
                });

            modelBuilder.Entity("FoodTypeEntityShoppingCartEntity", b =>
                {
                    b.Property<Guid>("FoodTypeEntitiesIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShoppingCartEntitiesIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FoodTypeEntitiesIdentifier", "ShoppingCartEntitiesIdentifier");

                    b.HasIndex("ShoppingCartEntitiesIdentifier");

                    b.ToTable("FoodTypeEntityShoppingCartEntity");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.FoodTypeEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationEntity");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.MenuEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationEntity");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.OrderEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClickAndEatApi.Db.Models.UserEntity", "UserEntity")
                        .WithOne("OrderEntity")
                        .HasForeignKey("ClickAndEatApi.Db.Models.OrderEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.ShoppingCartEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClickAndEatApi.Db.Models.UserEntity", "UserEntity")
                        .WithOne("ShoppingCartEntity")
                        .HasForeignKey("ClickAndEatApi.Db.Models.ShoppingCartEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.UserEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.OrganizationEntity", "OrganizationEntity")
                        .WithMany()
                        .HasForeignKey("OrganizationEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationEntity");
                });

            modelBuilder.Entity("FoodTypeEntityMenuEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.FoodTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("FoodTypeEntitiesIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClickAndEatApi.Db.Models.MenuEntity", null)
                        .WithMany()
                        .HasForeignKey("MenuEntitiesIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodTypeEntityOrderEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.FoodTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("FoodTypeEntitiesIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClickAndEatApi.Db.Models.OrderEntity", null)
                        .WithMany()
                        .HasForeignKey("OrderEntitiesIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodTypeEntityShoppingCartEntity", b =>
                {
                    b.HasOne("ClickAndEatApi.Db.Models.FoodTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("FoodTypeEntitiesIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClickAndEatApi.Db.Models.ShoppingCartEntity", null)
                        .WithMany()
                        .HasForeignKey("ShoppingCartEntitiesIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClickAndEatApi.Db.Models.UserEntity", b =>
                {
                    b.Navigation("OrderEntity")
                        .IsRequired();

                    b.Navigation("ShoppingCartEntity")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}