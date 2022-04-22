using System;
using Microsoft.EntityFrameworkCore;
using ShadowProperties.Models;

namespace ShadowProperties.Data
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Contact1> Contacts1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"Server=(localdb)\mssqllocaldb;Database=EF.ShadowEntityCore;Trusted_Connection=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Contact>().Property<DateTime?>("LastUpdated");
            modelBuilder.Entity<Contact>().Property<string>("LastUser");

            modelBuilder.Entity<Contact1>().Property<DateTime?>("LastUpdated");
            modelBuilder.Entity<Contact1>().Property<string>("LastUser");
            modelBuilder.Entity<Contact1>().Property<DateTime?>("CreatedAt");
            modelBuilder.Entity<Contact1>().Property<string>("CreatedBy");
            modelBuilder.Entity<Contact1>().Property<bool>("isDeleted");


            modelBuilder.Entity<Contact>(entity => entity.HasKey(e => e.ContactId));
            modelBuilder.Entity<Contact1>(entity => entity.HasKey(e => e.ContactId));

            /*
             * Setup filter on Contact1 model to show only active records.
             * Since IsDeleted is not in the model the string name is used.
             */
            modelBuilder.Entity<Contact1>()
                .HasQueryFilter(m =>
                    EF.Property<bool>(m, "isDeleted") == false);

            OnModelCreatingPartial(modelBuilder);

        }
        /// <summary>
        /// Set shadow properties, soft delete
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdated").CurrentValue = DateTime.Now;
                    entry.Property("LastUser").CurrentValue = Environment.UserName;

                    if (entry.Entity is Contact1 && entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                        entry.Property("CreatedBy").CurrentValue = Environment.UserName;
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Change state to modified and set delete flag
                    entry.State = EntityState.Modified;
                    entry.Property("isDeleted").CurrentValue = true;
                }

            }

            return base.SaveChanges();
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}