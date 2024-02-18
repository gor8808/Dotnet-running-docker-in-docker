using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {  }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        
        base.OnConfiguring(optionsBuilder);
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