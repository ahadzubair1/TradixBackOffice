using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IBrandCacheRepository
    {
        Task<List<Brand>> GetCachedListAsync();

        Task<Brand> GetByIdAsync(Guid brandId);
    }
}