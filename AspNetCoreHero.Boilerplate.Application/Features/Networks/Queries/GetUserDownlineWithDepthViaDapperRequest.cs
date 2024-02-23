using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;

public class GetUserDownlineWithDepthViaDapperRequest : IRequest<List<UserNetworkTreeDto>>
{
    public DefaultIdType UserId { get; set; }
    public bool IncludeSelf { get; set; } = true;
    public int Depth { get; set; } = 2;

    public GetUserDownlineWithDepthViaDapperRequest(DefaultIdType userId, bool includeSelf = true, int depth = 2) => (UserId, IncludeSelf, Depth) = (userId, includeSelf, depth);
}

public class GetUserDownlineWithDepthViaDapperRequestHandler : IRequestHandler<GetUserDownlineWithDepthViaDapperRequest, List<UserNetworkTreeDto>>
{
    private readonly IDapperRepository _repository;

    public GetUserDownlineWithDepthViaDapperRequestHandler(IDapperRepository repository) =>
        _repository = repository;

    public async Task<List<UserNetworkTreeDto>> Handle(GetUserDownlineWithDepthViaDapperRequest request, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserDownlineNetworkTreeWithDepth;
        var param = new { request.UserId, request.IncludeSelf, request.Depth };
        var entity = await _repository.QueryAsync<UserNetworkTree>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);

        //_ = entity ?? throw new NotFoundException(_t["GetUserNetwork for UserId {0} Not Found.", request.UserId]);


        return entity.Adapt<List<UserNetworkTreeDto>>();
        // Using mapster here throws a nullreference exception because of the "BrandName" property
        // in ProductDto and the product not having a Brand assigned.
        //return new UserNetworkTreeDto
        //{
        //    Id = entity.Id,
        //    BrandId = entity.BrandId,
        //    BrandName = string.Empty,
        //    Description = entity.Description,
        //    ImagePath = entity.ImagePath,
        //    Name = entity.Name,
        //    Rate = entity.Rate
        //};
    }
}