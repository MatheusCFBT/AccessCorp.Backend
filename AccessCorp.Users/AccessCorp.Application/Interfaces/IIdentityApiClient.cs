using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Domain.Entities;

namespace AccessCorpUsers.Application.Interfaces;

public interface IIdentityApiClient
{
    public Task<HttpResponseMessage> ViewAdministratorAsync(string identityId);
    public Task<HttpResponseMessage> RegisterAdministratorAsync(AdministratorIdentityRequest requestIdentity);
    public Task<HttpResponseMessage> UpdateAdministratorAsync(string identityId, Administrator administrator);
    public Task<Administrator> ExcludeAdministratorAsync(string identityId);
}