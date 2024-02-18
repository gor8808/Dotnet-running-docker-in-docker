using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"));

        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
       
        modelBuilder.Entity<User>()
            .Property(x => x.Name)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}