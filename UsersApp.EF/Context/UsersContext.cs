using Microsoft.EntityFrameworkCore;
using UsersApp.DAL.Models;

namespace UsersApp.EF.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Products);

            modelBuilder.Entity<Product>().HasMany(x => x.Users);

            BuildUserModel(modelBuilder);
        }

        private void BuildUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(300)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(300)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
