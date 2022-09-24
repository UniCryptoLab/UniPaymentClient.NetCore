using System;
using System.Runtime;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;
using UniPayment.Client;

namespace UniPayment.Client.Example
{
    public interface IUniPaymentClientProvider
    {
        public UniPaymentClient GetUniPaymentClient();
    }

    internal class UniPaymentClientProvider : IUniPaymentClientProvider
    {
        private string AppId { get; }

        private string ApiKey { get; }

        private bool IsSandbox { get; }

        public UniPaymentClientProvider(IConfiguration config)
        {
            this.AppId = config.GetValue<string>("AppId");
            this.ApiKey = config.GetValue<string>("ApiKey");
            this.IsSandbox = config.GetValue<bool>("IsSandbox");

        }

        public UniPaymentClientProvider(string appId,string apiKey,bool isSandbox)
        {
            AppId = appId;
            ApiKey = apiKey;
            isSandbox = isSandbox;
        }

        public UniPaymentClient GetUniPaymentClient()
        {
            if (string.IsNullOrWhiteSpace(AppId))
            {
                throw new Exception("A 'App Id' configuration value is required in the appsettings file.");
            }
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new Exception("A 'Api Key' configuration value is required in the appsettings file.");
            }
            return new UniPaymentClient(AppId, ApiKey, true);
        }
    }

    
}
