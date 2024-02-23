using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IRepository<Ticket> _repository;
        private readonly IDistributedCache _distributedCache;

        public TicketRepository(IDistributedCache distributedCache, IRepository<Ticket> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Ticket> Tickets => _repository.Entities;

        public async Task DeleteAsync(Ticket product)
        {
            await _repository.DeleteAsync(product);
            await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.GetKey(product.Id));
        }

        public async Task<Ticket> GetByIdAsync(Guid ticketId)
        {
            return await _repository.Entities.Where(p => p.Id == ticketId).FirstOrDefaultAsync();
        }

        public async Task<List<Ticket>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(Ticket ticket)
        {
            await _repository.AddAsync(ticket);
            await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.ListKey);
            return ticket.Id;
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            await _repository.UpdateAsync(ticket);
    //        await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.ListKey);
    //        await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.GetKey(ticket.Id));
        }
    }
}