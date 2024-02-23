using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : ITransientService
    {
        IQueryable<Transaction> List { get; }

        Task<List<Transaction>> GetListAsync();

        Task<Transaction> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(Transaction entity);

        Task UpdateAsync(Transaction entity);

        Task DeleteAsync(Transaction entity);
    }
}