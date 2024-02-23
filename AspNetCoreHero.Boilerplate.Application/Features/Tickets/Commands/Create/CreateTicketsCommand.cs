using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Tickets.Commands.Create
{
    public partial class CreateTicketsCommand : IRequest<Result<Guid>>
    {
        public string TicketTypeID { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Guid AssignedTo { get; set; }
        public Guid UserId { get; set; }
        public Guid CreatedBy { get; set; }
        // platform source
    }

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketsCommand, Result<Guid>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, IMapper mapper, 
                                        IAuthenticatedUserService authenticatedUserService)
        {
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Result<Guid>> Handle(CreateTicketsCommand request, CancellationToken cancellationToken)
        {
            request.Code = GenerateTicketCode();
            request.UserId = Guid.Parse(_authenticatedUserService.UserId);
            request.CreatedBy = Guid.Parse(_authenticatedUserService.UserId);
            var ticket = _mapper.Map<Ticket>(request);
            await _ticketRepository.InsertAsync(ticket);
            await _unitOfWork.SaveAndCommitAsync(_authenticatedUserService.UserId, cancellationToken);
            return Result<Guid>.Success(ticket.Id);
        }

        private static string GenerateTicketCode()
        {
            Random random = new();
            int rndTicketCode = 0;
            rndTicketCode = random.Next(100, 900000);
            var TicketCode = "TKT_" + Convert.ToString(rndTicketCode);

            return TicketCode;
        }
    }
}