using MicroserviceDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDemo.DataBase;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
            
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("DataSource=myApp.db");
    }
    
    public DbSet<Product> Products { get; set; }
}