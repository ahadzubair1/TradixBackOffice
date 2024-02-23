using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        IQueryable<Brand> Brands { get; }

        Task<List<Brand>> GetListAsync();

        Task<Brand> GetByIdAsync(Guid brandId);

        Task<Guid> InsertAsync(Brand brand);

        Task UpdateAsync(Brand brand);

        Task DeleteAsync(Brand brand);
    }
}