using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IMembershipRepository : ITransientService
    {
        IQueryable<Membership> List { get; }

        Task<List<Membership>> GetListAsync();

        Task<Membership> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(Membership entity);

        Task UpdateAsync(Membership entity);

        Task DeleteAsync(Membership entity);
    }
}