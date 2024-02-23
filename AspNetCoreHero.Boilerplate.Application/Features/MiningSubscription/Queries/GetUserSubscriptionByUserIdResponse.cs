namespace AspNetCoreHero.Boilerplate.Application.Features.MiningSubscription.Queries.GetById
{
    public class GetUserSubscriptionByUserIdResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int SubscriptionId { get; set; }
    }
}