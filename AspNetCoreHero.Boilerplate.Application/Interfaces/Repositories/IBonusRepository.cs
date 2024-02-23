using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IBonusRepository : ITransientService
    {
        IQueryable<UserBonus> List { get; }

        Task<List<UserBonus>> GetListAsync();

        Task<UserBonus> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(UserBonus entity);

        Task UpdateAsync(UserBonus entity);

        Task DeleteAsync(UserBonus entity);
    }
}