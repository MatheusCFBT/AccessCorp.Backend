using AccessCorpUsers.Application.Entities;
namespace AccessCorpUsers.Application.Interfaces;

public interface IIdentityApiClient
{
    public Task<AdministratorResponse> ViewAdministratorByEmailAsync(string email);    
    public Task<HttpResponseMessage> RegisterAdministratorAsync(AdministratorIdentityRequest requestIdentity);
    public Task<HttpResponseMessage> UpdateAdministratorAsync(string email, AdministratorIdentityRequest requestIdentity);
    public Task<HttpResponseMessage> ExcludeAdministratorAsync(string email);
}