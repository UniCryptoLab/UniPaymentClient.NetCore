using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class PayoutModel
    {
        [JsonProperty("payout_id")]
        public string PayoutId { get; set; }
        
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
        /// Crypto Payout Status
        /// </summary>
        [JsonProperty("status")]
        public EnumPayoutStatus Status { get; set; }
        
        /// <summary>
        /// Payout Create Time
        /// </summary>
        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// Payout Update Time
        /// </summary>
        [JsonProperty("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}