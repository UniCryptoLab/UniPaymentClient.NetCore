using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class CreateInvoiceRequest
    {
        /// <summary>
        /// Invoice Fiat Amount
        /// </summary>
        [JsonProperty("price_amount")]
        public float PriceAmount { get; set; }

        /// <summary>
        /// Invoice Fiat Currency
        /// </summary>
        [JsonProperty("price_currency")]
        public string PriceCurrency { get; set; }

        /// <summary>
        /// Crypto Payment Currency
        /// </summary>
        [JsonProperty("pay_currency")]
        public string PayCurrency { get; set; }

        /// <summary>
        /// Crypto Payment Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// IPN Callback Url
        /// </summary>
        [JsonProperty("notify_url")]
        public string NotifyURL { get; set; }

        /// <summary>
        /// Checkout Redirect Url
        /// </summary>
        [JsonProperty("redirect_url")]
        public string RedirectURL { get; set; }

        /// <summary>
        /// Merchant Side Order Id
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Invoice Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Invoice Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Invoice Language
        /// </summary>
        [JsonProperty("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// Merchant Side Ext Business Data
        /// </summary>
        [JsonProperty("ext_args")]
        public string ExtArgs { get; set; }

        /// <summary>
        /// Confirm Speed
        /// </summary>
        [JsonProperty("confirm_speed")]
        public string ConfirmSpeed { get; set; }
    }
}
