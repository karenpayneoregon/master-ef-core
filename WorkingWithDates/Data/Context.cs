﻿
// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkingWithDates.Data.Configurations;
using WorkingWithDates.Models;

#nullable disable

namespace WorkingWithDates.Data
{
    public partial class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Birthdays> Birthdays { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<TimeTable> TimeTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(BuildConnection());
            }
        }

        private static string BuildConnection()
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var sections = configuration.GetSection("database").GetChildren().ToList();

            return
                $"Data Source={sections[1].Value};" +
                $"Initial Catalog={sections[0].Value};" +
                $"Integrated Security={sections[2].Value}";

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BirthdaysConfiguration());
            modelBuilder.ApplyConfiguration(new EventsConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());

            modelBuilder.Entity<Person>(entity => {
                entity.ToTable("Person1");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}