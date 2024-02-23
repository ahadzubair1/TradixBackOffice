namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories
{
    public class BrandCacheRepository : IBrandCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IBrandRepository _brandRepository;

        public BrandCacheRepository(IDistributedCache distributedCache, IBrandRepository brandRepository)
        {
            _distributedCache = distributedCache;
            _brandRepository = brandRepository;
        }

        public async Task<Brand> GetByIdAsync(Guid brandId)
        {
            //string cacheKey = BrandCacheKeys.GetKey(brandId);
            //var brand = await _distributedCache.GetAsync<Brand>(cacheKey);
            //if (brand == null)
            //{
            //    brand = await _brandRepository.GetByIdAsync(brandId);
            //    Throw.Exception.IfNull(brand, "Brand", "No Brand Found");
            //    await _distributedCache.SetAsync(cacheKey, brand);
            //}
            var brand = await _brandRepository.GetByIdAsync(brandId);
            return brand;
        }

        public async Task<List<Brand>> GetCachedListAsync()
        {
            //string cacheKey = BrandCacheKeys.ListKey;
            //var brandList = await _distributedCache.GetAsync<List<Brand>>(cacheKey);
            //if (brandList == null)
            //{
            //    brandList = await _brandRepository.GetListAsync();
            //    await _distributedCache.SetAsync(cacheKey, brandList);
            //}
            var brandList = await _brandRepository.GetListAsync();
            return brandList;
        }
    }
}