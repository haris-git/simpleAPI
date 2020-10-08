﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleAPI.Context;

namespace SimpleAPI.Migrations
{
    [DbContext(typeof(SimpleApiContext))]
    partial class SimpleApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleAPI.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new { Id = 1, Company = "Company 1", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), FirstName = "John", LastName = "Doe" },
                        new { Id = 2, Company = "Company 2", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), FirstName = "Mark", LastName = "Kong" },
                        new { Id = 3, Company = "Company 3", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), FirstName = "Nick", LastName = "Cave" }
                    );
                });

            modelBuilder.Entity("SimpleAPI.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsPaid");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");

                    b.HasData(
                        new { Id = 1, ClientId = 2, Code = "ASD123", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), IsPaid = false, Status = "New Order" },
                        new { Id = 2, ClientId = 3, Code = "ZXC456", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), IsPaid = true, Status = "Completed" },
                        new { Id = 3, ClientId = 1, Code = "QWE135", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), IsPaid = false, Status = "Cancelled" },
                        new { Id = 4, ClientId = 1, Code = "JKL246", CreatedAt = new DateTime(2020, 10, 8, 15, 12, 47, 599, DateTimeKind.Local), IsPaid = true, Status = "Cancelled" }
                    );
                });

            modelBuilder.Entity("SimpleAPI.Entities.Order", b =>
                {
                    b.HasOne("SimpleAPI.Entities.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
