using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class PurchasedSubscriptionRepository : IPurchasedSubscriptionRepository
    {
        private readonly IRepository<UserPurchasedSubscriptions> _repository;
        private readonly IDistributedCache _distributedCache;

        public PurchasedSubscriptionRepository(IDistributedCache distributedCache, IRepository<UserPurchasedSubscriptions> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<UserPurchasedSubscriptions> Products => _repository.Entities;

        IQueryable<UserPurchasedSubscriptions> IPurchasedSubscriptionRepository.subscriptions => throw new NotImplementedException();

        public async Task DeleteAsync(UserPurchasedSubscriptions product)
        {
            await _repository.DeleteAsync(product);
            await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.GetKey(product.Id));
        }

      //  public async Task<Product> GetByIdAsync(Guid productId)
      //  {
       //     return await _repository.Entities..Where(p => p.Id == productId).FirstOrDefaultAsync();
       // }

        public async Task<List<UserPurchasedSubscriptions>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<UserPurchasedSubscriptions> InsertAsync(UserPurchasedSubscriptions subscription)
        {
            await _repository.AddAsync(subscription);
         //   await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.ListKey);
            return subscription;
        }


        public async Task UpdateAsync(UserPurchasedSubscriptions product)
        {
                await _repository.UpdateAsync(product);
       //     await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.ListKey);
       //     await _distributedCache.RemoveAsync(CacheKeys.ProductCacheKeys.GetKey(product.Id));
        }

        Task IPurchasedSubscriptionRepository.DeleteAsync(UserPurchasedSubscriptions brand)
        {
            throw new NotImplementedException();
        }

        Task<UserPurchasedSubscriptions> IPurchasedSubscriptionRepository.GetByIdAsync(DefaultIdType brandId)
        {
            throw new NotImplementedException();
        }

        Task<List<UserPurchasedSubscriptions>> IPurchasedSubscriptionRepository.GetListAsync()
        {
            throw new NotImplementedException();
        }

        /*Task IPurchasedSubscriptionRepository.UpdateAsync(UserPurchasedSubscriptions brand)
        {
            throw new NotImplementedException();
        }*/
    }
}