using Microsoft.EntityFrameworkCore;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class NetworkRepository : INetworkRepository
    {
        private readonly IRepository<Network> _repository;
        private readonly IDistributedCache _distributedCache;

        public NetworkRepository(IDistributedCache distributedCache, IRepository<Network> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Network> List => _repository.Entities;

        public async Task DeleteAsync(Network entity)
        {
            await _repository.DeleteAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(brand.Id));
        }

        public async Task<Network> GetByIdAsync(Guid id)
        {
            return await _repository.Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Network>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(Network entity)
        {
            await _repository.AddAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Network entity)
        {
            await _repository.UpdateAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(entity.Id));
        }
    }
}