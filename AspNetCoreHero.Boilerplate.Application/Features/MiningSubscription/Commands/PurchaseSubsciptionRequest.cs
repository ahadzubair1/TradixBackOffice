using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Features.Events.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Results;
using System.Threading.Tasks;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Abstractions.Interfaces;
using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Features.MiningSubscriptions.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Create;
using AspNetCoreHero.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;
namespace AspNetCoreHero.Boilerplate.Application.Features.MiningSubscriptions.Commands.Create
{
    public class UserPurchasedSubscriptions : IRequest<Result<Guid>>
    {
        public DefaultIdType UserId { get; set; }
        public string? UserName { get; set; }
        public string SubscriptionId { get; set; }
        [MaxLength(50)]
        public string SubscriptionName { get; set; }
        [MaxLength(500)]
        public bool IsActive { get; set; }

    }
    public class UserPurchasedSubscriptionsHandler : IRequestHandler<UserPurchasedSubscriptions, Result<Guid>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public UserPurchasedSubscriptionsHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UserPurchasedSubscriptionsHandler request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Brand>(request);
            await _brandRepository.InsertAsync(product);
            await _unitOfWork.SaveAndCommitAsync(cancellationToken: cancellationToken);
            return Result<Guid>.Success(product.Id);
        }

        public Task<Result<DefaultIdType>> Handle(UserPurchasedSubscriptions request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


    }