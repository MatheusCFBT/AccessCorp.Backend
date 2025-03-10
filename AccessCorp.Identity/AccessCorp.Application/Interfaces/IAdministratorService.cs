using AccessCorp.Application.Entities;

namespace AccessCorp.Application.Interfaces;

public interface IAdministratorService
{
    public Task<Result> ViewAdministrator(string id);
    public Task<Result> EditAdministrator(string id,AdministratorUpdateVM request);
    public Task<Result> ExcludeAdministrator(string id);
}