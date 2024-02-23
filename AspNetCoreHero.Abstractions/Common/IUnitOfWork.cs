using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Abstractions.Interfaces
{

    public interface IUnitOfWork : IDisposable, ITransientService
    {
        void BeginTransaction();
        Task Commit(CancellationToken cancellationToken = default);
        void Rollback();
        Task<int> SaveChangesAsync(string userId = null);
        Task SaveAndCommitAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}