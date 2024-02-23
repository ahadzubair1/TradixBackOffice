using AspNetCoreHero.Boilerplate.Application.DTOs.Dashboard;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IUserDashboardService : ITransientService
    {
        Task<UserDashboardDto> GetUserDashboard(Guid userId, CancellationToken cancellationToken);
    }
}