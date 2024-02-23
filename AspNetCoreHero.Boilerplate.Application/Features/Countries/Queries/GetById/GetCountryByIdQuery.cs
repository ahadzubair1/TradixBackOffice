using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Results;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Countries.Queries.GetById
{
    public class GetCountryByIdQuery : IRequest<Result<GetCountryByIdResponse>>
    {
        public Guid Id { get; set; }
        

        public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Result<GetCountryByIdResponse>>
        {
            private readonly IRepository<Country> _countryRepository;

            public GetCountryByIdQueryHandler(IRepository<Country> countryRepository)
            {
                _countryRepository = countryRepository;
            }

            public async Task<Result<GetCountryByIdResponse>> Handle(GetCountryByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _countryRepository.GetByIdAsync(query.Id);
                var mappedEntity = entity.Adapt<GetCountryByIdResponse>();
                return Result<GetCountryByIdResponse>.Success(mappedEntity);
            }
        }
    }
}