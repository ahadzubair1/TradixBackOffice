using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetById
{
    public class GetEventsByIdQuery : IRequest<Result<GetEventsByIdQuery>>
    {
        public Guid Id { get; set; }

        public class GetEventsByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdResponse>>
        {
            private readonly IBrandCacheRepository _brandCache;
            private readonly IMapper _mapper;

            public GetEventsByIdQueryHandler(IBrandCacheRepository EventsCache, IMapper mapper)
            {
                _brandCache = EventsCache;
                _mapper = mapper;
            }

            public async Task<Result<GetBrandByIdResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
            {
                var Events = await _brandCache.GetByIdAsync(query.Id);
                var mappedEvents = _mapper.Map<GetBrandByIdResponse>(Events);
                return Result<GetBrandByIdResponse>.Success(mappedEvents);
            }
        }
    }
}