using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnFunction.WebApi.Data;

public class AccessCorpDbContext : IdentityDbContext
{
    public AccessCorpDbContext(DbContextOptions<AccessCorpDbContext> options) : base(options) { }
    
}