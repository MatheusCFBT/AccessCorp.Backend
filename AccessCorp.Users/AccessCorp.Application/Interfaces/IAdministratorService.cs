using AccessCorpUsers.Application.Entities;

namespace AccessCorpUsers.Application.Interfaces
{
    public interface IAdministratorService
    {
        public Task<List<AdministratorVM>> ViewAllAdministrators(string email);
        public Task<AdministratorVM> ViewAdministratorById(Guid id);
        public Task<AdministratorVM> RegisterAdministrator(AdministratorVM request);
        public Task<AdministratorVM> UpdateAdministrator(Guid id, AdministratorVM request);
        public Task<AdministratorVM> ExcludeAdministrator(Guid id);
        public Task<AdministratorVM> GetAdminDoormansResidents(string email);
    }
}
