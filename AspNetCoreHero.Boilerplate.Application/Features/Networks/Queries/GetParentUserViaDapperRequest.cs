using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using Mapster;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;

public class GetParentUserViaDapperRequest : IRequest<UserNetworkTreeDto>
{
    public DefaultIdType UserId { get; set; }
    public NetworkPosition Position { get; set; }
    public IDbTransaction? Transaction { get; set; }

    public GetParentUserViaDapperRequest(DefaultIdType userId, NetworkPosition position, IDbTransaction? transaction = null) => (UserId, Position, Transaction) = (userId, position, transaction);
}

public class GetParentUserViaDapperRequestHandler : IRequestHandler<GetParentUserViaDapperRequest, UserNetworkTreeDto>
{
    private readonly IDapperRepository _repository;

    public GetParentUserViaDapperRequestHandler(IDapperRepository repository) =>
        _repository = repository;

    public async Task<UserNetworkTreeDto> Handle(GetParentUserViaDapperRequest request, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserNetworkFarNode;
        var param = new { request.UserId, request.Position };
        var entity = await _repository.QueryAsync<UserNetworkTree>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken, transaction: request.Transaction);

        

        return entity.FirstOrDefault().Adapt<UserNetworkTreeDto>();

    }
}