using Ardalis.Specification;
using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Results;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.MiningSubscription.Queries.GetById
{
    public class UserSubscriptionByUserIdSpec: Specification<UserPurchasedSubscriptions>
    {
        public UserSubscriptionByUserIdSpec(Guid userId)
        {
            Query.Where(usersubscription => usersubscription.UserId == userId);
        }
        public Guid UerId { get; set; }
    }
    public class GetUserSubscriptionByUserIdQuery : IRequest<Result<GetUserSubscriptionByUserIdResponse>>
    {
        public Guid UserId { get; set; }
        

        public class GetUserSubscriptionByUserIdQueryHandler : IRequestHandler<GetUserSubscriptionByUserIdQuery, Result<GetUserSubscriptionByUserIdResponse>>
        {
            private readonly IRepository<UserPurchasedSubscriptions> _repository;

            public GetUserSubscriptionByUserIdQueryHandler(IRepository<UserPurchasedSubscriptions> repository)
            {
                _repository = repository;
            }

            public async Task<Result<GetUserSubscriptionByUserIdResponse>> Handle(GetUserSubscriptionByUserIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.FirstOrDefaultAsync(new UserSubscriptionByUserIdSpec(query.UserId));
                var mappedEntity = entity.Adapt<GetUserSubscriptionByUserIdResponse>();
                return Result<GetUserSubscriptionByUserIdResponse>.Success(mappedEntity);
            }
        }
    }
}