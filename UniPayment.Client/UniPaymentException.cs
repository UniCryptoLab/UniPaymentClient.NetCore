using System;
using System.Text;

namespace UniPayment.Client
{
    public class UniPaymentApiException : UniPaymentException
    {
        private const string _defaultCode = "API_ERROR";
        private const string _defaultMessage = "Error when communication with UniPayment API";
        public UniPaymentApiException(Exception ex)
        :base(_defaultCode,_defaultMessage,ex)
        {
            
        }
    }
    public class UniPaymentException : Exception
    {
        public string Code { get; set; }

        public UniPaymentException(string code,string message) : base(message)
        {
            this.Code = code;
        }

        public UniPaymentException(string code , string message, Exception inner) : base(message, inner)
        {
            this.Code = code;
        }
    }
}
