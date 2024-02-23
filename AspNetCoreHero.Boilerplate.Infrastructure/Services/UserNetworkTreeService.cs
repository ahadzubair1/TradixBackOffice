using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application;
using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using Mapster;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Services;



public class UserNetworkTreeService : IUserNetworkTreeService
{
    private readonly IDapperRepository _repository;
    public UserNetworkTreeService(IDapperRepository repository)
    {
        _repository = repository;

    }
    public async Task<List<UserNetworkTreeDto>> GetDownlineAsync(DefaultIdType userId, NetworkPosition position, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserDownlineNetworkTree;
        var param = new { @UserId = userId, @Position = position };
        var entity = await _repository.QueryAsync<UserNetworkTree>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);

        _ = entity ?? throw new InvalidDataException(string.Format("Downline for UserId {0} Not Found.", userId));

        return entity.Adapt<List<UserNetworkTreeDto>>();
    }

    public async Task<UserNetworkTreeDto> GetFarUserAsync(DefaultIdType userId, NetworkPosition position, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserNetworkFarNode;
        var param = new { @UserId = userId, @Position = position };
        var entity = await _repository.QueryAsync<UserNetworkTree>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);

        _ = entity ?? throw new InvalidDataException(string.Format("Far user for UserId {0} Not Found.", userId));


        return entity.FirstOrDefault().Adapt<UserNetworkTreeDto>();
    }

    public async Task<List<UserNetworkTreeDto>> GetUplineAsync(DefaultIdType userId, CancellationToken cancellationToken)
    {
        string sql = AppConstants.Procedures.GetUserUplineNetworkTree;
        var param = new { @UserId = userId };
        var entity = await _repository.QueryAsync<UserNetworkTree>(sql, param: param, commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken);

        _ = entity ?? throw new InvalidDataException(string.Format("Upline for UserId {0} Not Found.", userId));


        return entity.Adapt<List<UserNetworkTreeDto>>();
    }
}