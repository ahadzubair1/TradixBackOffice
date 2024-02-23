using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Tickets.Commands.Delete
{
    public class DeleteTicketsCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteTicketsCommand, Result<Guid>>
        {
            private readonly ITicketRepository _ticketRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProductCommandHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
            {
                _ticketRepository = ticketRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Guid>> Handle(DeleteTicketsCommand command, CancellationToken cancellationToken)
            {
                var product = await _ticketRepository.GetByIdAsync(command.Id);
                await _ticketRepository.DeleteAsync(product);
                await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
                return Result<Guid>.Success(product.Id);
            }
        }
    }
}