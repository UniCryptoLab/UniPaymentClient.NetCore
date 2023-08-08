using System;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UniPayment.Client.Models;

namespace UniPayment.Client
{

    public partial class UniPaymentClient
    {
        public Response CheckIPN(string notify)
        {
            return this.CheckIPNAsync(notify).GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Check IPN
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<Response> CheckIPNAsync(string notify)
        {
            return this.Post<Response>("/v1.0/ipn", notify);
        }
    }
}