using System;
namespace UniPayment.Client.Models
{
    public class InvoiceDetailModel:InvoiceModel
    {
        /// <summary>
        /// Transactions Of Invoices
        /// </summary>
        public InvoiceTransactionModel[] Transactions { get; set; }
    }
}
