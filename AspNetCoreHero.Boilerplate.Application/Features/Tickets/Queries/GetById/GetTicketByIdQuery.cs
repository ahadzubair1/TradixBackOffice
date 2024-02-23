using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetById
{
    public class GetTicketByIdQuery : IRequest<Result<GetTicketByIdResponse>>
    {
        public Guid Id { get; set; }

        public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Result<GetTicketByIdResponse>>
        {
            private readonly IProductCacheRepository _productCache;
            private readonly IMapper _mapper;

            public GetTicketByIdQueryHandler(IProductCacheRepository productCache, IMapper mapper)
            {
                _productCache = productCache;
                _mapper = mapper;
            }

            public async Task<Result<GetTicketByIdResponse>> Handle(GetTicketByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productCache.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetTicketByIdResponse>(product);
                return Result<GetTicketByIdResponse>.Success(mappedProduct);
            }
        }
    }
}