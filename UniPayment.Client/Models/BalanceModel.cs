using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class BalanceModel
    {
        /// <summary>
        /// AssetType
        /// </summary>
        [JsonProperty("asset_type")] 
        public string AssetType { get; set; }

        /// <summary>
        /// Account Balance
        /// </summary>
        [JsonProperty("balance")] 
        public decimal Balance { get; set; }

        /// <summary>
        /// Account Frozen Balance
        /// </summary>
        [JsonProperty("frozen_balance")] 
        public decimal FrozenBalance { get; set; }

        /// <summary>
        /// Account Available Balance
        /// </summary>
        [JsonProperty("available")] 
        public decimal Available { get; set; }
    }
}