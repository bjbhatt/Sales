﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Sales.Persistence;
using System;

namespace Sales.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    [Migration("20180317171459_AddedURLForImagesToProduct")]
    partial class AddedURLForImagesToProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sales.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreateTimeStamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime?>("UpdateTimeStamp");

                    b.HasKey("Id");

                    b.ToTable("Customers","dbo");
                });

            modelBuilder.Entity("Sales.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTimeStamp");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<DateTime?>("UpdateTimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders","dbo");
                });

            modelBuilder.Entity("Sales.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTimeStamp");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.Property<DateTime?>("UpdateTimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail","dbo");
                });

            modelBuilder.Entity("Sales.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreateTimeStamp");

                    b.Property<string>("ImageURL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ThumbnailURL");

                    b.Property<decimal>("UnitPrice");

                    b.Property<DateTime?>("UpdateTimeStamp");

                    b.HasKey("Id");

                    b.ToTable("Products","dbo");
                });

            modelBuilder.Entity("Sales.Models.Order", b =>
                {
                    b.HasOne("Sales.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_Customer")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sales.Models.OrderDetail", b =>
                {
                    b.HasOne("Sales.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderDetail_Order")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Sales.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_OrderDetail_Product")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
