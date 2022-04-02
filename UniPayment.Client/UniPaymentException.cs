using System;
namespace UniPayment.Client
{
    public class UniPaymentException : Exception
    {
        public UniPaymentException(string message) : base(message)
        {
        }

        public UniPaymentException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
