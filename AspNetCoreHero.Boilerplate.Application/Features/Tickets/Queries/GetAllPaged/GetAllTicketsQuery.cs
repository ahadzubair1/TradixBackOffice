using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Results;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetAllPaged
{
    public class GetAllTicketsQuery : IRequest<PaginatedResult<GetAllTicketsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllTicketsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, PaginatedResult<GetAllTicketsResponse>>
    {
        private readonly ITicketRepository _repository;

        public GetAllTicketsQueryHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllTicketsResponse>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Ticket, GetAllTicketsResponse>> expression = e => new GetAllTicketsResponse
            {
                Id = e.Id,
                Description = e.Description,
                Subject = e.Subject,
                Code = e.Code,
                Status = e.Status,
                AssignedTo = (DefaultIdType)e.AssignedTo,
                CreatedDate = e.CreatedOn


               // Barcode = e.Barcode
            };
            var paginatedList = await _repository.Tickets
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}