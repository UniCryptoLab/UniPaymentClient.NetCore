using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class CreatePayoutRequestItem
    {
        /// <summary>
        /// Payout address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Payout amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}