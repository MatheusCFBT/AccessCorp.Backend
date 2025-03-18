using AccessCorpUsers.Application.Entities;

namespace AccessCorpUsers.Application.Interfaces
{
    public interface IResidentService
    {
        public Task<Result> ViewAllAdministrators(string email);
        public Task<ResidentVM> ViewAdministratorById(Guid id);
        public Task<Result> RegisterAdministrator(ResidentVM request);
        public Task<ResidentVM> UpdateAdministrator(string email, ResidentVM request);
        public Task<ResidentVM> ExcludeAdministrator(string id);
    }
}
