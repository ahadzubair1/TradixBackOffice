using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Create
{
    public partial class CreateProductCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.InsertAsync(product);
            await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
            return Result<Guid>.Success(product.Id);
        }
    }
}