using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class InvoiceTransactionModel
    {
        /// <summary>
        /// TransactionHash
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// BlockChain Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// Crypto Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// From Address
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// To Address
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction Confirmation Count
        /// </summary>
        [JsonProperty("confirmation_count")]
        public int ConfirmationCount { get; set; }

        /// <summary>
        /// Is Confirmed
        /// </summary>
        [JsonProperty("is_confirmed")]
        public bool IsConfirmed { get; set; }
    }
}
