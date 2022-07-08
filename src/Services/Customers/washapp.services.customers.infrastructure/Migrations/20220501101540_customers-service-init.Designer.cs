﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using washapp.services.customers.infrastructure.Database;

#nullable disable

namespace washapp.services.customers.infrastructure.Migrations
{
    [DbContext(typeof(WashAppDbContext))]
    [Migration("20220501101540_customers-service-init")]
    partial class customersserviceinit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("customers-service")
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LocalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Addresses", "customers-service");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Assortment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssortmentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssortmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<int>("WeightUnit")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AssortmentCategoryId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Assortments", "customers-service");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.AssortmentCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssortmentCategories", "customers-service");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Customers", "customers-service");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LocationColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations", "customers-service");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Address", b =>
                {
                    b.HasOne("washapp.services.customers.domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Assortment", b =>
                {
                    b.HasOne("washapp.services.customers.domain.Entities.AssortmentCategory", "AssortmentCategory")
                        .WithMany()
                        .HasForeignKey("AssortmentCategoryId");

                    b.HasOne("washapp.services.customers.domain.Entities.Customer", null)
                        .WithMany("AssortmentItems")
                        .HasForeignKey("CustomerId");

                    b.Navigation("AssortmentCategory");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Customer", b =>
                {
                    b.HasOne("washapp.services.customers.domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("washapp.services.customers.domain.Entities.Customer", b =>
                {
                    b.Navigation("AssortmentItems");
                });
#pragma warning restore 612, 618
        }
    }
}
