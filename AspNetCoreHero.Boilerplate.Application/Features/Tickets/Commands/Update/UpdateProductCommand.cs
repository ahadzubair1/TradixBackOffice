using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Update
{
    public class UpdateTicketCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Guid AssignedTo { get; set; }

        public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Result<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ITicketRepository _ticketRepository;

            public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
            {
                _ticketRepository = ticketRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Guid>> Handle(UpdateTicketCommand command, CancellationToken cancellationToken)
            {
                var ticket = await _ticketRepository.GetByIdAsync(command.Id);

                if (ticket == null)
                {
                    return Result<Guid>.Fail($"Product Not Found.");
                }
                else
                {
                    ticket.Code = command.Code ?? ticket.Code;
                    ticket.Subject = command.Subject ?? ticket.Subject;
                    ticket.Description = command.Description ?? ticket.Description;
                    ticket.AssignedTo = (command.AssignedTo == Guid.Empty) ? ticket.AssignedTo : command.AssignedTo;
                    await _ticketRepository.UpdateAsync(ticket);
                    await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
                    return Result<Guid>.Success(ticket.Id);
                }
            }
        }
    }
}