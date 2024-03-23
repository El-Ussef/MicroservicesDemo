using IdentityServiceApi.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceApi.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("DataSource=myApp.db");
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}