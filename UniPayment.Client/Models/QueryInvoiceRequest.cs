using System;
using Newtonsoft.Json;

namespace UniPayment.Client.Models
{
    public class QueryInvoiceRequest
    {
        public string InvoiceId { get; set; }

        public string AppId { get; set; }
        public string OrderId { get; set; }

        public EnumInvoiceStatus? Status { get; set; }

        public int? PageNo { get; set; }

        public int? PageSize { get; set; }

        public bool IsAsc { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
    }
}