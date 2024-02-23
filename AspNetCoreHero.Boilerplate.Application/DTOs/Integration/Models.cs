using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.DTOs.Integration
{
    public class GasPriceResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public GasPriceData Result { get; set; }
        public GasPriceResponse()
        {
            Result = new GasPriceData();
        }
    }

    public class GasPriceData
    {
        //public string LastBlock { get; set; }
        public string SafeGasPrice { get; set; } = "0.0";
        public string ProposeGasPrice { get; set; } = "0.0";
        public string FastGasPrice { get; set; } = "0.0";
        public string SuggestBaseFee { get; set; } = "0.0";
        //public string GasUsedRatio { get; set; }
        public string UsdPrice { get; set; } = "0.0";
    }
    public class SubscriptionTypes
    {
        public Int32 subscriptionId { get; set; }
        public string subscriptionType { get; set; }
        public string subscriptionDuration { get; set; }
        public float subscriptionAmount { get; set; }
    }

    public class ApiResponse
    {
        public List<SubscriptionTypes> Payload { get; set; }
        public bool Success { get; set; }
    }

    public class UserSubscriptions
    {
        public Int32 subscriptionId { get; set; } = 15;
        public string purchasedAt { get; set; } = "1695050318000";
        public string subscriptionPurchaseDate { get; set; } = "2023-09-18T15:18:38.000Z";
        public string subscriptionStartDate { get; set; } = "2023-09-18T15:18:38.000Z";
        public string subscriptionStartsAt { get; set; } = "2023-09-18T15:18:38.000Z";
        public string subscriptionExpiry { get; set; } = "2023-09-18T15:18:38.000Z";
        public string subscriptionEndsAt { get; set; } = "2023-09-18T15:18:38.000Z";
        public string subscriptionEndDate { get; set; } = "2026-09-18T15:24:38.000Z";
        public string subscriptionType { get; set; } = "Mining Grid License (12 Months)";

    }



    public class EventRequest
    {
        public string EventType { get; set; }
        public string EventData { get; set; }
    }


    public class EventResponse
    {
        public string EventName { get; set; }
        public string EventTime { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }

}
