using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class CancelWithdrawalRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}