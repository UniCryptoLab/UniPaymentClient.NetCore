using System;
using  Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class WithdrawalFee
    {
        /// <summary>
        /// AssetType
        /// </summary>
        [JsonProperty("asset_type")]
        public string AssetType { get; set; }
        
        /// <summary>
        /// Network
        /// </summary>
        [JsonProperty("network")]
        public  string Network { get; set; }

        /// <summary>
        /// Fee Type
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("fee_type")]
        public EnumFeeType FeeType { get; set; }
        
        /// <summary>
        /// Withdraw Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        
        /// <summary>
        /// Min Fee (Used in bank withdrawal)
        /// </summary>
        [JsonProperty("min_txn_fee")]
        public decimal? MinTxnFee { get; set; }
    }
}