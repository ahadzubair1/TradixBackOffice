using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Results;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Countries.Queries.GetAllCached
{
    public class GetAllCountriesQuery : IRequest<Result<List<GetAllCountriesResponse>>>
    {
    }

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, Result<List<GetAllCountriesResponse>>>
    {
        private readonly IRepository<Country> _repository;

        public GetAllCountriesQueryHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetAllCountriesResponse>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.ListAsync();
            var mapped = list.Adapt<List<GetAllCountriesResponse>>();
            return Result<List<GetAllCountriesResponse>>.Success(mapped);
        }
    }
}