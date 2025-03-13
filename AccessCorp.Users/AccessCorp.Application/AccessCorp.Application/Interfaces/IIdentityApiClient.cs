using AccessCorp.Domain.Entities;

namespace AccessCorp.Domain.Interfaces;

public interface IIdentityApiClient
{
    public Task<Administrator> ViewAdministratorAsync(string identityId);
    public Task<Administrator> RegisterAdministratorAsync(Administrator administrator);
    public Task<Administrator> UpdateAdministratorAsync(string identityId, Administrator administrator);
    public Task<Administrator> ExcludeAdministratorAsync(string identityId);
}