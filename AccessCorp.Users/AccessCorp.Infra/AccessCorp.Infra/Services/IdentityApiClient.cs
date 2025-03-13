using AccessCorp.Domain.Interfaces;

namespace AccessCorp.Domain.Services;

public class IdentityApiClient : IIdentityApiClient
{
    private readonly HttpClient _httpClient;
    
    public IdentityApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
}