using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces
{
    public interface IUserNetworkTreeService : ITransientService
    {
        Task<List<UserNetworkTreeDto>> GetDownlineAsync(Guid userId, NetworkPosition position, CancellationToken cancellationToken);
        Task<List<UserNetworkTreeDto>> GetUplineAsync(Guid userId, CancellationToken cancellationToken);
        Task<UserNetworkTreeDto> GetFarUserAsync(Guid userId, NetworkPosition position, CancellationToken cancellationToken);
    }
}