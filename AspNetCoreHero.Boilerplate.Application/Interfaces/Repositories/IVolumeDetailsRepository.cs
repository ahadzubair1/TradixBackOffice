using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IVolumeDetailsRepository : ITransientService
    {
        IQueryable<VolumeDetails> List { get; }

        Task<List<VolumeDetails>> GetListAsync();

        Task<VolumeDetails> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(VolumeDetails entity);

        Task UpdateAsync(VolumeDetails entity);

        Task DeleteAsync(VolumeDetails entity);
    }
}