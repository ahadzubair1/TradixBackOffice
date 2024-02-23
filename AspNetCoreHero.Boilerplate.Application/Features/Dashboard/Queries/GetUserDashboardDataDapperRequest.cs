using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.DTOs.Dashboard;
using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using Mapster;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VM.WebApi.Domain.App;

namespace AspNetCoreHero.Boilerplate.Application.Features.Dashboard.Queries;

public class GetUserDashboardDataDapperRequest : IRequest<UserDashboardDto>
{
    public DefaultIdType UserId { get; set; }
    public GetUserDashboardDataDapperRequest(DefaultIdType userId) => (UserId) = (userId);
}

public class GetUserDashboardDataDapperRequestHandler : IRequestHandler<GetUserDashboardDataDapperRequest, UserDashboardDto>
{
    private readonly IDapperRepository _repository;

    public GetUserDashboardDataDapperRequestHandler(IDapperRepository repository) =>
        _repository = repository;

    public async Task<UserDashboardDto> Handle(GetUserDashboardDataDapperRequest request, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserDashboardData;
        var param = new { request.UserId };
        var entity = await _repository.QueryAsync<UserDashboardData>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);


        return entity.FirstOrDefault().Adapt<UserDashboardDto>();
    }
}