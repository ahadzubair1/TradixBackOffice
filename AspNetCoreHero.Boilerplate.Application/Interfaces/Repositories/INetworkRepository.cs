using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface INetworkRepository : ITransientService
    {
        IQueryable<Network> List { get; }

        Task<List<Network>> GetListAsync();

        Task<Network> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(Network entity);

        Task UpdateAsync(Network entity);

        Task DeleteAsync(Network entity);
    }
}