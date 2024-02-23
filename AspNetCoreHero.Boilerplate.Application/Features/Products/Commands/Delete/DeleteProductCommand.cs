using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Guid>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                await _productRepository.DeleteAsync(product);
                await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
                return Result<Guid>.Success(product.Id);
            }
        }
    }
}