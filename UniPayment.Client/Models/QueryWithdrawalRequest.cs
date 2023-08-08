using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class QueryWithdrawalRequest
    {
        public string Network { get; set; }
        
        public string AssetType { get; set; }
        
        public EnumWithdrawStatus? Status { get; set; }

        public int? PageNo { get; set; }

        public int? PageSize { get; set; }

        public bool IsAsc { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
    }
}