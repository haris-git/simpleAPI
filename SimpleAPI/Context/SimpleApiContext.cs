using Microsoft.EntityFrameworkCore;
using SimpleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Context
{
    public class SimpleApiContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

        public SimpleApiContext(DbContextOptions<SimpleApiContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasData(
                new Client()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Company = "Company 1",
                    CreatedAt = DateTime.Now
                },
                new Client()
                {
                    Id = 2,
                    FirstName = "Mark",
                    LastName = "Kong",
                    Company = "Company 2",
                    CreatedAt = DateTime.Now
                },
                new Client()
                {
                    Id = 3,
                    FirstName = "Nick",
                    LastName = "Cave",
                    Company = "Company 3",
                    CreatedAt = DateTime.Now
                });

            modelBuilder.Entity<Order>()
                .HasData(
                new Order()
                {
                    Id = 1,
                    Code = "ASD123",
                    Status = "New Order",
                    IsPaid = false,
                    ClientId = 2,
                    CreatedAt = DateTime.Now
                },
                new Order()
                {
                    Id = 2,
                    Code = "ZXC456",
                    Status = "Completed",
                    IsPaid = true,
                    ClientId = 3,
                    CreatedAt = DateTime.Now
                },
                new Order()
                {
                    Id = 3,
                    Code = "QWE135",
                    Status = "Cancelled",
                    IsPaid = false,
                    ClientId = 1,
                    CreatedAt = DateTime.Now
                },
                new Order()
                {
                    Id = 4,
                    Code = "JKL246",
                    Status = "Cancelled",
                    IsPaid = true,
                    ClientId = 1,
                    CreatedAt = DateTime.Now
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
