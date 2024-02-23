using Microsoft.EntityFrameworkCore;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly IRepository<Membership> _repository;
        private readonly IDistributedCache _distributedCache;

        public MembershipRepository(IDistributedCache distributedCache, IRepository<Membership> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Membership> List => _repository.Entities;

        public async Task DeleteAsync(Membership entity)
        {
            await _repository.DeleteAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(brand.Id));
        }

        public async Task<Membership> GetByIdAsync(Guid id)
        {
            return await _repository.Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Membership>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(Membership entity)
        {
            await _repository.AddAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Membership entity)
        {
            await _repository.UpdateAsync(entity);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.BrandCacheKeys.GetKey(entity.Id));
        }
    }
}