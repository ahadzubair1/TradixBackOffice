using AspNetCoreHero.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IRepository<Wallet> _repository;
        private readonly IDistributedCache _distributedCache;

        public WalletRepository(IDistributedCache distributedCache, IRepository<Wallet> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Wallet> List => _repository.Entities;

        public async Task DeleteAsync(Wallet entity)
        {
            await _repository.DeleteAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(brand.Id));
        }

        public async Task<Wallet> GetByIdAsync(Guid id)
        {
            return await _repository.Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Wallet>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(Wallet entity)
        {
            await _repository.AddAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Wallet entity)
        {
            await _repository.UpdateAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(entity.Id));
        }
    }
}