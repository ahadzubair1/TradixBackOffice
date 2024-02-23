using Ardalis.Specification;
using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Results;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.MiningSubscription.Queries.GetById
{
    public class SubscriptionTypeBySubscriptionIdSpec: Specification<SubscriptionType>
    {
        public SubscriptionTypeBySubscriptionIdSpec(int subscriptionId)
        {
            Query.Where(subscription => subscription.SubscriptionId == subscriptionId);
        }
        public int SubscriptionId { get; set; }
    }
    public class GetSubscriptionBySubscriptionIdQuery : IRequest<Result<GetSubscriptionBySubscriptionIdResponse>>
    {
        public int SubscriptionId { get; set; }
        

        public class GetSubscriptionBySubscriptionIdQueryHandler : IRequestHandler<GetSubscriptionBySubscriptionIdQuery, Result<GetSubscriptionBySubscriptionIdResponse>>
        {
            private readonly IRepository<SubscriptionType> _repository;

            public GetSubscriptionBySubscriptionIdQueryHandler(IRepository<SubscriptionType> repository)
            {
                _repository = repository;
            }

            public async Task<Result<GetSubscriptionBySubscriptionIdResponse>> Handle(GetSubscriptionBySubscriptionIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.FirstOrDefaultAsync(new SubscriptionTypeBySubscriptionIdSpec(query.SubscriptionId));
                var mappedEntity = entity.Adapt<GetSubscriptionBySubscriptionIdResponse>();
                return Result<GetSubscriptionBySubscriptionIdResponse>.Success(mappedEntity);
            }
        }
    }
}