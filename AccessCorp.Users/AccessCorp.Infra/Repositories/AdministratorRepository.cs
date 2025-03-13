using AccessCorpUsers.Domain.Entities;
using AccessCorpUsers.Domain.Interfaces;
using AccessCorpUsers.Infra.Context;

namespace AccessCorpUsers.Infra.Repositories;

public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(AccessCorpUsersDbContext context) : base(context) { }

}