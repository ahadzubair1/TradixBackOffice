namespace AspNetCoreHero.Abstractions.Repository
{
    public interface IRepository_Old<T> : ICommandRepository<T>, IQueryRepository<T> where T : class { }
}