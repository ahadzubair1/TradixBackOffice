namespace AspNetCoreHero.Boilerplate.Application.Features.MiningSubscription.Queries.GetById
{
    public class GetSubscriptionBySubscriptionIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SubscriptionId { get; set; }
        public long Amount { get; set; }
    }
}