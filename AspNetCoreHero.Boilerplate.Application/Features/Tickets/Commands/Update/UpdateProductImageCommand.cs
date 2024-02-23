/*using AspNetCoreHero.Boilerplate.Application.Exceptions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Update
{
    public class UpdateProductImageCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }

        public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand, Result<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProductRepository _productRepository;

            public UpdateProductImageCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Guid>> Handle(UpdateProductImageCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    throw new ApiException($"Product Not Found.");
                }
                else
                {
                    product.Image = command.Image;
                    await _productRepository.UpdateAsync(product);
                    await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
                    return Result<Guid>.Success(product.Id);
                }
            }
        }
    }
}*/