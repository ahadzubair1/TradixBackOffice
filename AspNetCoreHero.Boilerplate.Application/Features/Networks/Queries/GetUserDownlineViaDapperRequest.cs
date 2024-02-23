using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;

public class GetUserDownlineViaDapperRequest : IRequest<List<UserNetworkTreeDto>>
{
    public DefaultIdType UserId { get; set; }
    public bool IncludeSelf { get; set; } = true;

    public GetUserDownlineViaDapperRequest(DefaultIdType userId, bool includeSelf = true) => (UserId, IncludeSelf) = (userId, includeSelf);
}

public class GetUserDownlineViaDapperRequestHandler : IRequestHandler<GetUserDownlineViaDapperRequest, List<UserNetworkTreeDto>>
{
    private readonly IDapperRepository _repository;

    public GetUserDownlineViaDapperRequestHandler(IDapperRepository repository) =>
        _repository = repository;

    public async Task<List<UserNetworkTreeDto>> Handle(GetUserDownlineViaDapperRequest request, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserDownlineNetworkTree;
        var param = new { request.UserId, request.IncludeSelf };
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