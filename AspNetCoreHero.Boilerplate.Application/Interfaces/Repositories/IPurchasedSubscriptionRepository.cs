using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IPurchasedSubscriptionRepository
    {
        IQueryable<UserPurchasedSubscriptions> subscriptions { get; }

        Task<List<UserPurchasedSubscriptions>> GetListAsync();

        Task<UserPurchasedSubscriptions> GetByIdAsync(Guid brandId);

        Task<UserPurchasedSubscriptions> InsertAsync(UserPurchasedSubscriptions brand);

        Task UpdateAsync(UserPurchasedSubscriptions product);

        Task DeleteAsync(UserPurchasedSubscriptions brand);
    }
}
