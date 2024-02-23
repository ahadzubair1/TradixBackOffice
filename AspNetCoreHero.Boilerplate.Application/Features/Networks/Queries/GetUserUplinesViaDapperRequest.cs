using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;

public class GetUserUplinesViaDapperRequest : IRequest<List<UserNetworkTreeDto>>
{
    public DefaultIdType UserId { get; set; }
    public bool IncludeSelf { get; set; } = true;

    public GetUserUplinesViaDapperRequest(DefaultIdType userId, bool includeSelf = true) => (UserId, IncludeSelf) = (userId, includeSelf);
}

public class GetUserUplinesViaDapperRequestHandler : IRequestHandler<GetUserUplinesViaDapperRequest, List<UserNetworkTreeDto>>
{
    private readonly IDapperRepository _repository;

    public GetUserUplinesViaDapperRequestHandler(IDapperRepository repository) =>
        (_repository) = (repository);

    public async Task<List<UserNetworkTreeDto>> Handle(GetUserUplinesViaDapperRequest request, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserDownlineNetworkTree;
        var param = new { request.UserId, request.IncludeSelf };
        var entity = await _repository.QueryAsync<UserNetworkTree>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);

        return entity.Adapt<List<UserNetworkTreeDto>>();
    }
}