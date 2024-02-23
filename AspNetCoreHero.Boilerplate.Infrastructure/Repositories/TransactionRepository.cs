using Microsoft.EntityFrameworkCore;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IRepository<Transaction> _repository;
        //private readonly IDistributedCache _distributedCache;

        public TransactionRepository(IDistributedCache distributedCache, IRepository<Transaction> repository)
        {
            //_distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Transaction> List => _repository.Entities;

        public async Task DeleteAsync(Transaction entity)
        {
            await _repository.DeleteAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(brand.Id));
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await _repository.Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Transaction>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(Transaction entity)
        {
            await _repository.AddAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Transaction entity)
        {
            await _repository.UpdateAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(entity.Id));
        }
    }
}