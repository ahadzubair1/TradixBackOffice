using System.ComponentModel.DataAnnotations;

namespace AspNetCoreHero.Boilerplate.Application.DTOs.Identity
{
    public class UserSubscriptionInfo
    {
        public int SubscriptionId { get; set; }
        public long PurchasedAt { get; set; }
        public DateTime SubscriptionPurchaseDate { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public long SubscriptionStartsAt { get; set; }
        public long SubscriptionExpiry { get; set; }
        public long SubscriptionEndsAt { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public DateTime SubscriptionExpiryDate { get; set; }
        public string SubscriptionType { get; set; }
    }

    public class DataSubsciptions
    {
        public List<UserSubscriptionInfo> Subscriptions { get; set; }
    }

}