using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IWalletRepository : ITransientService
    {
        IQueryable<Wallet> List { get; }

        Task<List<Wallet>> GetListAsync();

        Task<Wallet> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(Wallet entity);

        Task UpdateAsync(Wallet entity);

        Task DeleteAsync(Wallet entity);
    }
}