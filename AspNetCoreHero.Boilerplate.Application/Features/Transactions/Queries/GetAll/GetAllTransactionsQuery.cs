using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Transactions.Queries.GetAll
{
    public class GetAllTransactionsQuery : IRequest<Result<List<GetAllTransactionsResponse>>>
    {
        public GetAllTransactionsQuery()
        {
        }
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, Result<List<GetAllTransactionsResponse>>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllTransactionsResponse>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListAsync();
            var mappedList = _mapper.Map<List<GetAllTransactionsResponse>>(list);
            return Result<List<GetAllTransactionsResponse>>.Success(mappedList);
        }
    }
}