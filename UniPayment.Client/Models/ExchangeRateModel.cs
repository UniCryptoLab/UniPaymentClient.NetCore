using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class ExchangeRateModel
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}
