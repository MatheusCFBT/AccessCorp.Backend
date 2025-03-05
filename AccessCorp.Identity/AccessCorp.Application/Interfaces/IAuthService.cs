using AccessCorp.Application.Entities;

namespace AccessCorp.Application.Interfaces;

public interface IAuthService
{
    public Task<AdministratorResponseVM> GenerateJWTAdmin(string email);
    public Task<DoormanResponseVM> GenerateJWTDoorman(string email);
    public Task<bool> ValidateCep(string cep);
}