using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IRankRepository : ITransientService
    {
        IQueryable<Rank> List { get; }

        Task<List<Rank>> GetListAsync();

        Task<Rank> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(Rank entity);

        Task UpdateAsync(Rank entity);

        Task DeleteAsync(Rank entity);
    }
}