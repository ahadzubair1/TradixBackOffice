using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IProductCacheRepository
    {
        Task<List<Product>> GetCachedListAsync();

        Task<Product> GetByIdAsync(Guid brandId);
    }
}