
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Create
{
    public partial class CreateBrandCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<Guid>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Brand>(request);
            await _brandRepository.InsertAsync(product);
            await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
            return Result<Guid>.Success(product.Id);
        }
    }
}