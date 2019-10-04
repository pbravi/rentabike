﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rentabike.data;

namespace rentabike.data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191001144317_initial7")]
    partial class initial7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rentabike.model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Client");

                    b.Property<int?>("RentalId");

                    b.HasKey("Id");

                    b.HasIndex("RentalId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("rentabike.model.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompositeRentalId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int>("RentalTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CompositeRentalId");

                    b.HasIndex("RentalTypeId");

                    b.ToTable("Rentals");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Rental");
                });

            modelBuilder.Entity("rentabike.model.RentalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsComposite");

                    b.HasKey("Id");

                    b.ToTable("RentalTypes");

                    b.HasData(
                        new { Id = 1, Description = "Rent by hour", IsComposite = false },
                        new { Id = 2, Description = "Rent by day", IsComposite = false },
                        new { Id = 3, Description = "Rent by week", IsComposite = false },
                        new { Id = 4, Description = "Rent familiar group (30% discount)", IsComposite = true },
                        new { Id = 5, Description = "Rental group", IsComposite = true }
                    );
                });

            modelBuilder.Entity("rentabike.model.strategies.Strategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("MaxCompositeSize");

                    b.Property<int>("MinCompositeSize");

                    b.HasKey("Id");

                    b.ToTable("Strategy");

                    b.HasData(
                        new { Id = 1, Description = "Family Strategy", MaxCompositeSize = 5, MinCompositeSize = 3 }
                    );
                });

            modelBuilder.Entity("rentabike.model.strategies.StrategyRentalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Discount");

                    b.Property<double>("Price");

                    b.Property<int>("PriceStrategyId");

                    b.Property<int>("RentalTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PriceStrategyId");

                    b.HasIndex("RentalTypeId");

                    b.ToTable("FamilyPriceStrategyRentalTypes");

                    b.HasData(
                        new { Id = 1, Discount = 0.0, Price = 5.0, PriceStrategyId = 1, RentalTypeId = 1 },
                        new { Id = 2, Discount = 0.0, Price = 20.0, PriceStrategyId = 1, RentalTypeId = 2 },
                        new { Id = 3, Discount = 0.0, Price = 60.0, PriceStrategyId = 1, RentalTypeId = 3 },
                        new { Id = 4, Discount = 0.3, Price = 0.0, PriceStrategyId = 1, RentalTypeId = 4 },
                        new { Id = 5, Discount = 0.0, Price = 0.0, PriceStrategyId = 1, RentalTypeId = 5 }
                    );
                });

            modelBuilder.Entity("rentabike.model.CompositeRental", b =>
                {
                    b.HasBaseType("rentabike.model.Rental");


                    b.ToTable("CompositeRental");

                    b.HasDiscriminator().HasValue("CompositeRental");
                });

            modelBuilder.Entity("rentabike.model.LeafRental", b =>
                {
                    b.HasBaseType("rentabike.model.Rental");

                    b.Property<int?>("CompositeRentalId1");

                    b.HasIndex("CompositeRentalId1");

                    b.ToTable("LeafRental");

                    b.HasDiscriminator().HasValue("LeafRental");
                });

            modelBuilder.Entity("rentabike.model.Order", b =>
                {
                    b.HasOne("rentabike.model.Rental", "Rental")
                        .WithMany()
                        .HasForeignKey("RentalId");
                });

            modelBuilder.Entity("rentabike.model.Rental", b =>
                {
                    b.HasOne("rentabike.model.CompositeRental")
                        .WithMany("Childrens")
                        .HasForeignKey("CompositeRentalId");

                    b.HasOne("rentabike.model.RentalType", "Type")
                        .WithMany()
                        .HasForeignKey("RentalTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("rentabike.model.strategies.StrategyRentalType", b =>
                {
                    b.HasOne("rentabike.model.strategies.Strategy", "PriceStrategy")
                        .WithMany("StrategyRentalTypes")
                        .HasForeignKey("PriceStrategyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("rentabike.model.RentalType", "RentalType")
                        .WithMany()
                        .HasForeignKey("RentalTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("rentabike.model.LeafRental", b =>
                {
                    b.HasOne("rentabike.model.CompositeRental", "CompositeRental")
                        .WithMany()
                        .HasForeignKey("CompositeRentalId1");
                });
#pragma warning restore 612, 618
        }
    }
}
