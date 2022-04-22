using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockingPeopleLibrary.Models;

namespace MockingPeopleLibrary.Data
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// Belongs in appsettings.json
        /// </summary>
        private static string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=EF.People;Trusted_Connection=True";

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            var entry = Entry<TEntity>(entity);

            if (entry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
                entry.State = EntityState.Modified;
            }

            return base.Update(entity);
        }
    }
}
