using AccessCorpUsers.Application.Entities;

namespace AccessCorpUsers.Application.Interfaces
{
    public interface IAdministratorService
    {
        public Task<Result> ViewAllAdministrators(string email);
        public Task<AdministratorVM> ViewAdministratorById(Guid id);
        public Task<Result> RegisterAdministrator(AdministratorVM request);
        public Task<AdministratorVM> UpdateAdministrator(Guid id, AdministratorVM request);
        public Task<AdministratorVM> ExcludeAdministrator(Guid id);
        public Task<AdministratorVM> GetAdminDoormansResidents(string email);
    }
}
