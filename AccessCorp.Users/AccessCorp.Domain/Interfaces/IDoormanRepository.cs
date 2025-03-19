using AccessCorpUsers.Domain.Entities;
using System.Linq.Expressions;

namespace AccessCorpUsers.Domain.Interfaces
{
    public interface IDoormanRepository : IRepository<Doorman>
    {
        Task<Doorman> GetDoormanByEmail(string email);
        Task<IEnumerable<Doorman>> GetDoormanByCep(string cep);
    }
}
