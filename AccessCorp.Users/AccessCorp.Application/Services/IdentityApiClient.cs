using AccessCorpUsers.Application.Configuration;
using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace AccessCorpUsers.Application.Services;

public class IdentityApiClient : IIdentityApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IdentityApiSettings _identityApiSettings;

    
    public IdentityApiClient(HttpClient httpClient, IOptions<IdentityApiSettings> identityApiSettings, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _identityApiSettings = identityApiSettings.Value;
        _httpClient.BaseAddress = new Uri(_identityApiSettings.BaseUrl);
    }

    public async Task<HttpResponseMessage> ViewAdministratorAsync(string identityId)
    {
        var result = await _httpClient.GetAsync($"identity/v1/administrator/view-administrator?id={identityId}");

        return result;
    }

    public async Task<HttpResponseMessage> RegisterAdministratorAsync(AdministratorIdentityRequest requestIdentity)
    {
        var jsonContent = JsonSerializer.Serialize(requestIdentity);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        string token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        
        token = token.Substring("Bearer ".Length).Trim();
        

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await _httpClient.PostAsync($"identity/v1/administrator/register-administrator", content);

        if (!result.IsSuccessStatusCode)
        {
            string errorResponse = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro {result.StatusCode}: {errorResponse}");
            throw new Exception($"Erro na requisição: {result.StatusCode} - {errorResponse}");
        }

        return result;
    }

    public Task<HttpResponseMessage> UpdateAdministratorAsync(string identityId, Administrator administrator)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator> ExcludeAdministratorAsync(string identityId)
    {
        throw new NotImplementedException();
    }
}