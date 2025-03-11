using Microsoft.EntityFrameworkCore;

namespace AccessCorp.Infra.Context;

public class AccessCorpUsersDbContext : DbContext
{
    public AccessCorpUsersDbContext(DbContextOptions<AccessCorpUsersDbContext> options) : base(options) { }
    
    Db
}