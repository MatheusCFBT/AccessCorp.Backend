using AccessCorpUsers.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessCorpUsers.Application.Interfaces
{
    public interface IAdministratorService
    {
        public Task<List<AdministratorVM>> ViewAllAdministrators();
        public Task<AdministratorVM> ViewAdministratorById(Guid id);
        public Task<AdministratorVM> RegisterAdministrator(AdministratorVM request);
        public Task<AdministratorVM> UpdateAdministrator(Guid id, AdministratorVM request);
        public Task<AdministratorVM> ExcludeAdministrator(Guid id);
    }
}
