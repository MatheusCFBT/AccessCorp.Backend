using AccessCorpUsers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessCorpUsers.Infra.Context;

public class AccessCorpUsersDbContext : DbContext
{
    public AccessCorpUsersDbContext(DbContextOptions<AccessCorpUsersDbContext> options) : base(options) 
    {
    }

    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Doorman> Doormans { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Resident> Residents { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccessCorpUsersDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}