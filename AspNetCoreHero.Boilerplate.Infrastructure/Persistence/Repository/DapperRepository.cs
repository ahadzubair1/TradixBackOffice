using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.Abstractions.Repository;
using Dapper;
using System.Data;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Repository;

public class DapperRepository : IDapperRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DapperRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
    where T : class, IEntity =>
        (await _dbContext.Connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType))
            .AsList();

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class, IEntity
    {
        return await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class, IEntity
    {

        return _dbContext.Connection.QuerySingleAsync<T>(sql, param, transaction);
    }
}