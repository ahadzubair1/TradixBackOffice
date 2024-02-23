using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Tickets { get; }

        Task<List<Ticket>> GetListAsync();

        Task<Ticket> GetByIdAsync(Guid ticketId);

        Task<Guid> InsertAsync(Ticket ticket);

        Task UpdateAsync(Ticket ticket);

        Task DeleteAsync(Ticket ticket);
    }
}