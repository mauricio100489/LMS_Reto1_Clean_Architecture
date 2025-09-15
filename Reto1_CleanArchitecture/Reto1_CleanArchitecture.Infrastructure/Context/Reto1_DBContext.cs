using Microsoft.EntityFrameworkCore;
using Reto1_CleanArchitecture.Infrastructure.Mappings;
using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Infrastructure.Context
{
    public class Reto1_DBContext(DbContextOptions<Reto1_DBContext> options) : DbContext(options)
    {
        #region DbSets
        public virtual DbSet<Authentications> Authentications { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Mappings
            modelBuilder.ApplyConfiguration(new AuthenticationsMap());
            modelBuilder.ApplyConfiguration(new ContactsMap());
            #endregion
        }
    }
}
