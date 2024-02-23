using AspNetCoreHero.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly IRepository<MiningRaceEvents> _repository;
        private readonly IDistributedCache _distributedCache;

        public EventsRepository(IDistributedCache distributedCache, IRepository<MiningRaceEvents> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<MiningRaceEvents> Events => _repository.Entities;

        public async Task DeleteAsync(MiningRaceEvents events)
        {
            await _repository.DeleteAsync(events);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(events.Id));
        }

        public async Task<MiningRaceEvents> GetByIdAsync(Guid eventsId)
        {
            return await _repository.Entities.Where(p => p.Id == eventsId).FirstOrDefaultAsync();
        }

        public async Task<List<MiningRaceEvents>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(MiningRaceEvents events)
        {
            await _repository.AddAsync(events);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            return events.Id;
        }

        public async Task UpdateAsync(MiningRaceEvents events)
        {
            await _repository.UpdateAsync(events);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(events.Id));
        }
    }
}