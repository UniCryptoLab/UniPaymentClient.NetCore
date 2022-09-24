using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;


namespace UniPayment.Client.Example
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HandlerController : ControllerBase
    {
        
        private string _appId = string.Empty;
        private string _apiKey = string.Empty;
        private bool _isSandbox = false;
        
        public HandlerController(IConfiguration configuration)
        {
            _appId = configuration.GetValue<string>("AppId");
            _apiKey = configuration.GetValue<string>("ApiKey");
            _isSandbox = configuration.GetValue<bool>("isSandbox");
        }
        
        [ActionName("demo")]
        public string Demo()
        {
            return  "hello world";
        }
        
        [ActionName("notify")]
        public async Task<string> Post()
        {
            var notify = await new StreamReader(Request.Body).ReadToEndAsync();

            var client = new UniPaymentClient(_appId, _apiKey, true);
            var result = await client.CheckIPNAsync(notify);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        
    }
}