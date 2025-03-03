using OnFunction.Application.Entities;

namespace OnFunction.Application.Interfaces;

public interface IAuthService
{
    public Task<AdministratorResponseVM> GenerateJWTAdmin(string email);
    public Task<DoormanResponseVM> GenerateJWTDoorman(string email);
}