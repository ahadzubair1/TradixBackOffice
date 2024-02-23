using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface ITicketCacheRepository
    {
        Task<List<Ticket>> GetCachedListAsync();

        Task<Ticket> GetByIdAsync(Guid ticketId);
    }
}