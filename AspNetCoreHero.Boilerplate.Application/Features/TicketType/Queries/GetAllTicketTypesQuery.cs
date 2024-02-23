using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Results;
using Mapster;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Countries.Queries.GetAllCached
{
    public class GetAllTicketTypesQuery : IRequest<Result<List<GetAllTicketTypeResponse>>>
    {
    }

    public class GetAllTicketTypesHandler : IRequestHandler<GetAllTicketTypesQuery, Result<List<GetAllTicketTypeResponse>>>
    {
        private readonly IRepository<TicketType> _repository;

        public GetAllTicketTypesHandler(IRepository<TicketType> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetAllTicketTypeResponse>>> Handle(GetAllTicketTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.ListAsync();
            var mapped = list.Adapt<List<GetAllTicketTypeResponse>>();
            return Result<List<GetAllTicketTypeResponse>>.Success(mapped);
        }
    }
}