using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class CreateWithdrawalRequest
    {
        public CreateWithdrawalRequest()
        {
            //Default Withdraw Amount Include Fee
            this.IncludeFee = true;
        }
        
        /// <summary>
        /// Blockchain Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }
        
        /// <summary>
        /// Withdraw Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
        
        /// <summary>
        /// Withdraw AssetType
        /// </summary>
        [JsonProperty("asset_type")]
        public string AssetType { get; set; }
        
        /// <summary>
        /// Withdraw Amount
        /// </summary>
        [JsonProperty("amount")]
        public float Amount { get; set; }
        
        /// <summary>
        /// The extra tag need( Ripple, Monero)
        /// </summary>
        [JsonProperty("dest_tag")]
        public string DestTag { get; set; }
        
        /// <summary>
        /// Notify Url
        /// </summary>
        [JsonProperty("notify_url")]
        public string NotifyUrl { get; set; }
        
        /// <summary>
        /// Note
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }
        
        /// <summary>
        /// Auto Confirm Without Email Confirmation
        /// </summary>
        [JsonProperty("auto_confirm")]
        public bool AutoConfirm { get; set; }

        /// <summary>
        /// IF set to false, sender pays withdraw fee
        /// </summary>
        [JsonProperty("include_fee")]
        public bool IncludeFee { get; set; }
    }
}