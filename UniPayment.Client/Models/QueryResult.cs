using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniPayment.Client.Models
{
    public class QueryResult<T> where T : class
    {
        [JsonPropertyName("models")]
        public List<T> Models { get; set; }

        [JsonPropertyName("page_no")]
        public int PageNo { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("page_count")]
        public int PageCount { get; set; }
    }
}
