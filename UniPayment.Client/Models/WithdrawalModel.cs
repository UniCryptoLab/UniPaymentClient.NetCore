using System;
using  Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class WithdrawalModel
    {
        /// <summary>
        /// Withdrawal Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// 网络类型
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// 资产类型
        /// </summary>
        [JsonProperty("asset_type")]
        public string AssetType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 出金地址
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("status")]
        public EnumWithdrawStatus Status { get; set; }

        /// <summary>
        /// 出金交易Hash
        /// </summary>
        [JsonProperty("txn_hash")]
        public string TxnHash { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }
    }
}