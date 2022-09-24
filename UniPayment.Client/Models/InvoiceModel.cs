using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class InvoiceModel
    {
        /// <summary>
        /// App Id
        /// </summary>
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        /// <summary>
        /// InvoiceId
        /// </summary>
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Client Side Order Id
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Invoice Fiat Amount
        /// </summary>
        [JsonProperty("price_amount")]
        public decimal PriceAmount { get; set; }

        /// <summary>
        /// Invoice Fiat Currency
        /// </summary>
        [JsonProperty("price_currency")]
        public string PriceCurrency { get; set; }

        /// <summary>
        /// BlockChian Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// BlockChain Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Crypto Payment Amount
        /// </summary>
        [JsonProperty("pay_amount")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// Crypto Payment Currency
        /// </summary>
        [JsonProperty("pay_currency")]
        public string PayCurrency { get; set; }

        /// <summary>
        /// Exchnage Rate
        /// </summary>
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }


        /// <summary>
        /// Amount Already Paid
        /// </summary>
        [JsonProperty("paid_amount")]
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Invoice Create Time
        /// </summary>
        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Invoice Expiration Time
        /// </summary>
        [JsonProperty("expiration_time")]
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// Invoice Confrim Speed
        /// None High Medium Low
        /// </summary>
        [JsonProperty("confirm_speed")]
        public EnumConfirmSpeed ConfirmSpeed { get; set; }

        /// <summary>
        /// Invoice Status
        /// New Paid Confirmed Complete Expired Invalid
        /// </summary>
        [JsonProperty("status")]
        public EnumInvoiceStatus Status { get; set; }

        /// <summary>
        /// Invoice Error Status
        /// None PaidLate PaidPartial PaidOver Marked
        /// </summary>
        [JsonProperty("error_status")]
        public EnumInvoiceErrorStatus ErrorStatus { get; set; }

        /// <summary>
        /// Invoice CheckOut Url
        /// </summary>
        [JsonProperty("invoice_url")]
        public string InvoiceUrl { get; set; }


    }
}
