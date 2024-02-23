using AspNetCoreHero.Boilerplate.Application;
using AspNetCoreHero.Boilerplate.Application.DTOs.Dashboard;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using Mapster;
using VM.WebApi.Domain.App;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Services
{

    public class UserDashboardService : IUserDashboardService
    {
        private readonly IDapperRepository _repository;
        public UserDashboardService(IDapperRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDashboardDto> GetUserDashboard(Guid userId, CancellationToken cancellationToken)
        {
            string sql = AppConstants.Procedures.GetUserDashboardData;
            var param = new { @UserId = userId };
            var entity = await _repository.QueryAsync<UserDashboardData>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);

            _ = entity ?? throw new InvalidDataException(string.Format("Upline for UserId {0} Not Found.", userId));


            return entity.Adapt<UserDashboardDto>();
        }
    }
}
