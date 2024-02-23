
using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Boilerplate.Application.Features.MiningSubscription.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using AspNetCoreHero.Boilerplate.Infrastructure.Attributes;
using AspNetCoreHero.Results;
using MassTransit;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using static AspNetCoreHero.Boilerplate.Application.Constants.Permissions;
namespace AspNetCoreHero.Boilerplate.Application.Features.Events.Commands.Create
{
    // User fields
    //       public object in place of dictionary 
    // double deserialization
    public partial class CreateMiningRaceEventCommand : IRequest<Result<Guid>>
    {
        [Column("Event Type")]
        public string EventType { get; set; }
        // Property to hold the deserialized JSON object
        public string Username { get; set; }
        [Required]
        [CustomEmailAddress]
        public string Email { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [Required]
        public string NFTId { get; set; }
        public string ReferralCodeLeft { get; set; }
        public string ReferralCodeAuto { get; set; }
        public string ReferralCodeRight { get; set; }
        public string ReferredBy { get; set; }
        public object Data { get; set; }

        // Method to deserialize the Data property based on EventType
        public T DeserializeData<T>()
        {
            try
            {
                switch (EventType)
                {
                    case "SubscriptionPurchase":
                        return JsonConvert.DeserializeObject<T>(Data.ToString());
                    case "EventType2": // Sign Up
                        return JsonConvert.DeserializeObject<T>((string)Data); // Replace "EventType2" with the actual event type
                                                                               // Add more cases for other event types as needed
                    default:
                        throw new InvalidOperationException($"Unsupported EventType: {EventType}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

    }

    public class CreateMiningRaceEventHandler : IRequestHandler<CreateMiningRaceEventCommand, Result<Guid>>
    {
        private readonly IEventsRepository _eventRepository;
        private readonly IRepository<UserPurchasedSubscriptions> _purchasedSubscriptionRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IMediator _mediator;
        // private readonly UserManager<ApplicationUser> _userManager;

        public CreateMiningRaceEventHandler(IEventsRepository eventRepository,
            IRepository<UserPurchasedSubscriptions> purchasedRepository,
            IUnitOfWork unitOfWork, IMapper mapper,
            IIdentityService identityService, IMediator mediator)
        {
            _eventRepository = eventRepository;
            _purchasedSubscriptionRepository = purchasedRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _identityService = identityService;
            _mediator = mediator;

        }

        public async Task<Result<Guid>> Handle(CreateMiningRaceEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Creating an instance of MiningRaceEvents with static data
                MiningRaceEvents events = new MiningRaceEvents
                {
                    EventType = request.EventType,
                    Data = request.Data.ToString().Replace(" ", "") // Replace with your static JSON data
                };

                await _eventRepository.InsertAsync(events);

                // Use the DeserializeData function to get the deserialized object
                DataSubsciptions userSubscriptionInfo = request.DeserializeData<DataSubsciptions>();

                // Now 'userSubscriptionInfo' contains the deserialized data, and you can work with it
                foreach (var subscription in userSubscriptionInfo.Subscriptions)
                {
                    Console.WriteLine($"Subscription ID: {subscription.SubscriptionId}");
                    Console.WriteLine($"Subscription Type: {subscription.SubscriptionType}");

                    // Check if SubscriptionId is empty and handle accordingly
                    var query = new GetSubscriptionBySubscriptionIdQuery { SubscriptionId = subscription.SubscriptionId };
                    var result = await _mediator.Send(query);

                    if (subscription.SubscriptionId == 0 || result.Data == null)  // Assuming 0 is an indicator of an empty SubscriptionId
                    {
                        return Result<Guid>.Fail("Invalid Subscription Id");
                    }
                    else
                    {

                        UserPurchasedSubscriptions ups = new UserPurchasedSubscriptions
                        {
                            UserName = request.Username,
                            UserId = Guid.Parse(request.UserId),
                            Amount = result.Data.Amount,
                            SubscriptionId = subscription.SubscriptionId,
                            PurchasedAt = subscription.PurchasedAt,
                            SubscriptionPurchaseDate = subscription.SubscriptionPurchaseDate,
                            SubscriptionStartDate = subscription.SubscriptionStartDate,
                            SubscriptionStartsAt = subscription.SubscriptionStartsAt,
                            SubscriptionExpiry = subscription.SubscriptionExpiry,
                            SubscriptionEndsAt = subscription.SubscriptionEndsAt,
                            SubscriptionEndDate = subscription.SubscriptionEndDate,
                            SubscriptionExpiryDate = subscription.SubscriptionExpiryDate,
                            SubscriptionType = subscription.SubscriptionType,

                        };

                        //var userPurchased = new GetUserSubscriptionByUserIdQuery { UserId = Guid.Parse(request.UserId) };

                        Guid userId = string.IsNullOrEmpty(request.UserId) ? Guid.Empty: Guid.Parse(request.UserId);
                        var userAlreadyPurchased = await _purchasedSubscriptionRepository.FirstOrDefaultAsync(new UserSubscriptionByUserIdSpec(userId));//await _mediator.Send(userPurchased); await 
                        if (userAlreadyPurchased != null)
                        {
                            userAlreadyPurchased.Amount = result.Data.Amount;
                            userAlreadyPurchased.SubscriptionId = subscription.SubscriptionId;
                            userAlreadyPurchased.PurchasedAt = subscription.PurchasedAt;
                            userAlreadyPurchased.SubscriptionPurchaseDate = subscription.SubscriptionPurchaseDate;
                            userAlreadyPurchased.SubscriptionStartDate = subscription.SubscriptionStartDate;
                            userAlreadyPurchased.SubscriptionStartsAt = subscription.SubscriptionStartsAt;
                            userAlreadyPurchased.SubscriptionExpiry = subscription.SubscriptionExpiry;
                            userAlreadyPurchased.SubscriptionEndsAt = subscription.SubscriptionEndsAt;
                            userAlreadyPurchased.SubscriptionEndDate = subscription.SubscriptionEndDate;
                            userAlreadyPurchased.SubscriptionExpiryDate = subscription.SubscriptionExpiryDate;
                            userAlreadyPurchased.SubscriptionType = subscription.SubscriptionType;
                            await _purchasedSubscriptionRepository.UpdateAsync(userAlreadyPurchased);
                        }
                        else
                        {
                    
                            await _identityService.ReassignReferralAsync(request.UserId, request.ReferralCodeLeft, request.ReferralCodeAuto, request.ReferralCodeRight);
                            await _purchasedSubscriptionRepository.AddAsync(ups);
                            //await _identityService.AssingNetworkAsync(request.UserId, referredBy, NetworkPosition.Auto);
                            await _identityService.ReassignNftId(request.UserId, request.NFTId);

                            await _identityService.AssingParentAsync(request.UserId, request.ReferredBy, NetworkPosition.Auto);
    
                        }


                    }
                    // X-APIToken
                }
                return Result<Guid>.Success(0);//userSubscriptionInfo.SubscriptionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result<Guid>.Fail("An error occurred while processing the request.");
            }
        }
    }
}