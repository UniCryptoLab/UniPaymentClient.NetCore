using System;
using System.Text.Json.Serialization;
namespace UniPayment.Client.Models
{
    public class Response<T> where T : class
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
