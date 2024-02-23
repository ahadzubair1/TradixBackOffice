using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using System.Threading.Tasks;

namespace VM.WebApi.Application.App.UserMemberships;



public class PurchaseUserMembershipRequest : IRequest<Guid>
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType MembershipId { get; set; }
    public DefaultIdType WalletId { get; set; }
}


public class PurchaseUserMembershipRequestHandler : IRequestHandler<PurchaseUserMembershipRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepository<UserMembership> _repository;
    private readonly IRepository<Wallet> _walletRepository;
    private readonly IRepository<Membership> _membershipRepository;

    private readonly IRepository<Purchase> _purchaseRepository;
    private readonly IRepository<Transaction> _transactionRepository;

    private readonly IRepository<DirectReferral> _drbRepository;
    private readonly IRepository<NetworkVolume> _networkVolumeRepository;
    private readonly IRepository<VolumeDetails> _userVolumeDetailsRepository;

    private readonly IRepository<RankVolume> _rankVolumeRepository;

    private readonly IIdentityService _userService;
    private readonly IUserNetworkTreeService _userNetworkTreeService;
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseUserMembershipRequestHandler(
        IUnitOfWork unitOfWork,
        IRepository<UserMembership> repository,
        IRepository<Wallet> walletRepository,
        IRepository<Membership> membershipRepository,
        IRepository<Purchase> purchaseRepository,
        IRepository<Transaction> transactionRepository,
        IIdentityService userService,
        IRepository<DirectReferral> drbRepository,
        IRepository<NetworkVolume> networkVolumeRepository,
        IRepository<VolumeDetails> userNetworkVolumeRepository,
        IRepository<RankVolume> rankVolumeRepository,
        IUserNetworkTreeService userNetworkTreeService)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _walletRepository = walletRepository;
        _membershipRepository = membershipRepository;
        _transactionRepository = transactionRepository;
        _purchaseRepository = purchaseRepository;
        _drbRepository = drbRepository;
        _networkVolumeRepository = networkVolumeRepository;
        _userVolumeDetailsRepository = userNetworkVolumeRepository;
        _rankVolumeRepository = rankVolumeRepository;
        _userService = userService;
        _userNetworkTreeService = userNetworkTreeService;
        _userService = userService;
    }

    public async Task<Guid> Handle(PurchaseUserMembershipRequest request, CancellationToken cancellationToken)
    {
        //var entity = request.Adapt<UserMembership>();

        // Business Logic

        /*
        1. check if membership purchasing allowed from the wallet
        2. check membership amount
        3. check user wallet balance
        4. deduct balance from user wallet
        5. create a new entry in UserMembership table
        6. create a new entry in Investment table
        7. create a new entry in Transaction table
        8. create a new entry in UserDirectReferralBonus table
        9. add amount in respective wallets based distribution ratio of (x/100) * y
        10. add an entry in Network Volume table (This is transaction table)
        11. update UserVolumeDetails information for user (Rename table to UserVolumeDetails)
        12. add an entry in Rank Volume table (This is transaction table)
        13. update UserVolumeDetails information
        */

        try
        {
            _unitOfWork.BeginTransaction();
            var user = await _userService.GetAsync(request.UserId.ToString(), cancellationToken);

            
            Transaction transaction = new Transaction
            {
                UserId = request.UserId,
                //Amount = membership.Price,
                TransactionStatus = TransactionStatus.Completed,
                TransactionType = TransactionType.Purchase,
                TransactionCategory = TransactionType.Purchase.MapTransactionCategory(),
                Source = request.WalletId.ToString(),
            };

            var insertedTransaction = await _transactionRepository.AddAsync(transaction);

            //wallet.Balance = remainingAmount;
            //await _walletRepository.UpdateAsync(wallet);
            var membership = await _membershipRepository.GetByIdAsync(request.MembershipId, cancellationToken);

            Purchase investment = new Purchase
            {
                Membership = membership,
                Amount = membership.Price,
                //Wallet = wallet,
                UserId = request.UserId,
                Cap = membership.Cap,
                ExpiryInterval = membership.ExpiryInterval,
                TransactionId = insertedTransaction.Id,
            };


            if (membership.EnableDirectReferralBonus)
            {
                if (user.ReferredBy.HasValue)
                {
                    DirectReferral referralBonus = new DirectReferral
                    {
                        MembershipTypeId = membership.MembershipTypeId.Value,
                        Amount = 0.01 * membership.DirectReferralBonusPrecentage * membership.Price,
                        MembershipPrice = membership.Price,
                        Percentage = membership.DirectReferralBonusPrecentage,
                        Membership = membership,
                        Position = user.Position,
                        UserId = request.UserId,
                        //IsRepurchased = repurchased,
                        ReferredBy = user.ReferredBy.Value
                    };

                    await _drbRepository.AddAsync(referralBonus);
                }
            }

            NetworkVolume networkVolume = new NetworkVolume
            {
                MembershipId = membership.Id,
                MembershipTypeId = membership.MembershipTypeId.Value,
                ReferredBy = user.ReferredBy,
                UserId = request.UserId,
                Position = user.Position,
                Volume = membership.Price
            };

            await _networkVolumeRepository.AddAsync(networkVolume);


            RankVolume rankVolume = new RankVolume
            {
                MembershipTypeId = membership.MembershipTypeId.Value,
                ReferredBy = user.ReferredBy,
                Position = user.Position,
                Volume = membership.RankVolume,
                MembershipId = membership.Id,
                UserId = request.UserId,
            };

            await _rankVolumeRepository.AddAsync(rankVolume);

            UserMembership userMembership = new UserMembership
            {
                UserId = request.UserId,
                Membership = membership,
                Investment = investment,
                //DirectReferralBonus = referralBonus
            };

            var entity = await _repository.AddAsync(userMembership, cancellationToken);


            // Generate Volumes

            //var tree = await _userNetworkTreeService.GetUplineAsync(user.Id, cancellationToken);

            //if(tree != null)
            //{
            //    foreach(var node in tree)
            //    {
            //        if(node.UserId != user.Id)
            //        {
            //            var volumeDetails = await _userVolumeDetailsRepository.FirstOrDefaultAsync(new VolumeDetailsByUserIdSpec(user.Id, membership.Id));

            //            if (volumeDetails == null)
            //            {
            //                volumeDetails = new VolumeDetails();
            //                volumeDetails.UserId = user.Id;
            //                volumeDetails.MembershipTypeId = membership.MembershipTypeId.Value;
            //            }
            //        }
            //    }
            //}

            await _unitOfWork.SaveAndCommitAsync();
            return entity.Id;
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}