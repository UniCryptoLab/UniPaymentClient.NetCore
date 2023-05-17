using System;
namespace UniPayment.Client.Models
{
    public enum EnumConfirmSpeed
    {
        None = 0,
        High = 1,
        Medium = 2,
        Low = 3
    }
    
    public enum EnumInvoiceStatus
    {
        New = 1,
        Paid = 2,
        Confirmed = 3,
        Complete = 4,
        Expired = 5,
        Invalid = 6,
    }
    
    public enum EnumInvoiceErrorStatus
    {
        None = 0,
        PaidLate = 1,
        PaidPartial = 2,
        PaidOver = 3,
        Marked = 4,
    }

    public enum EnumPayoutStatus
    {
        Pending = 1,
        Processing = 6,
        Complete = 7
    }
}
