using System;
using  Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class WithdrawalModel
    {
        /// <summary>
        /// Withdrawal Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// Withdraw Network Type
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// Asset Type
        /// </summary>
        [JsonProperty("asset_type")]
        public string AssetType { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("status")]
        public EnumWithdrawStatus Status { get; set; }

        /// <summary>
        /// Withdraw TxnHash
        /// </summary>
        [JsonProperty("txn_hash")]
        public string TxnHash { get; set; }

        /// <summary>
        ///  Note
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Create Time
        /// </summary>
        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }
    }
}