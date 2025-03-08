using AccessCorp.Application.Entities;

namespace AccessCorp.Application.Interfaces;

public interface IAdministratorService
{
    public Task<Result> EditAdministrator(AdministratorUpdateVM request);
}