using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                return;
            }

            _transaction = _context.Database.BeginTransaction();
        }

        public Task<int> SaveChangesAsync(string userId = null)
        {
            return _context.SaveChangesAsync(userId);
        }

        public Task Commit(CancellationToken cancellationToken = default)
        {
            
            if(_transaction != null )
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
            return Task.CompletedTask;
          
        }

        public async Task SaveAndCommitAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(userId);
            await Commit();
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                return;
            }

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction == null)
            {
                return;
            }

            _transaction.Dispose();
            _transaction = null;
        }

    }
}