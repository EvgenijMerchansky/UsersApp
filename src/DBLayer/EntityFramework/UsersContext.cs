using Microsoft.EntityFrameworkCore;
using Users.Example.DBLayer.Models;

namespace Users.Example.DBLayer.EntityFramework;

public class UsersContext(DbContextOptions<UsersContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasMany(x => x.Products);

        BuildUserModel(modelBuilder);
    }

    private void BuildUserModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();
    }
}