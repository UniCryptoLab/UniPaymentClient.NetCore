using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class CreatePayoutRequest
    {
        /// <summary>
        /// Crypto Payment Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }
        
        /// <summary>
        /// Crypto Payout Asset
        /// </summary>
        [JsonProperty("asset_type")]
        public string AssetType { get; set; }
        
        /// <summary>
        /// Crypto Payout Items
        /// </summary>
        [JsonProperty("items")]
        public CreatePayoutRequestItem[] Items { get; set; }
        
    }
}