using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Delete
{
    public class DeleteBrandCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result<Guid>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
            {
                _brandRepository = brandRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Guid>> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
            {
                var product = await _brandRepository.GetByIdAsync(command.Id);
                await _brandRepository.DeleteAsync(product);
                await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
                return Result<Guid>.Success(product.Id);
            }
        }
    }
}