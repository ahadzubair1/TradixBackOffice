using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.Abstractions.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories;

// Inherited from Ardalis.Specification's RepositoryBase<T>
public class ApplicationDbRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    private readonly DbContext _dbContext;
    public ApplicationDbRepository(ApplicationDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    // We override the default behavior when mapping to a dto.
    // We're using Mapster's ProjectToType here to immediately map the result from the database.
    // This is only done when no Selector is defined, so regular specifications with a selector also still work.
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}