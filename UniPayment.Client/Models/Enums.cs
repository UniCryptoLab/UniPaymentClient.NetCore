﻿using System;
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

    public enum EnumWithdrawStatus
    {
        Pending = 1,
        Cancel = 2,
        Confirm = 3,
        Reject = 4,
        Approve = 5,
        Success = 6,
        Fail = 7,
    }

    public enum EnumFeeType
    {
        Free =1,
        Ratio=2,
        Fix =3,
    }
}
