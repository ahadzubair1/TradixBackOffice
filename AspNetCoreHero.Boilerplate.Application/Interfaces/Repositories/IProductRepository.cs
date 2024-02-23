using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        Task<List<Product>> GetListAsync();

        Task<Product> GetByIdAsync(Guid productId);

        Task<Guid> InsertAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}