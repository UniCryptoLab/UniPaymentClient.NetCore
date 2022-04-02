using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class CurrencyModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_fiat")]
        public bool IsFiat { get; set; }
    }
}
