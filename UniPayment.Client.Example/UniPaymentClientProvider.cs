using System;
using System.Runtime;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using UniPayment.Client;

namespace UniPayment.Client.Example
{
    public interface IUniPaymentClientProvider
    {
        public Client GetUniPaymentClient();
    }

    internal class UniPaymentClientProvider : IUniPaymentClientProvider
    {
        private string AppId { get; }

        private string ApiKey { get; }

        private string ApiHost { get; }

        public UniPaymentClientProvider(IConfiguration config)
        {
            this.AppId = config.GetValue<string>("AppId");
            this.ApiKey = config.GetValue<string>("ApiKey");
            this.ApiHost = config.GetValue<string>("ApiHost");

        }

        public UniPaymentClientProvider(string appId,string apiKey,string apiHost)
        {
            AppId = appId;
            ApiKey = apiKey;
            ApiHost = apiHost;
        }

        public Client GetUniPaymentClient()
        {
            if (string.IsNullOrWhiteSpace(AppId))
            {
                throw new Exception("A 'App Id' configuration value is required in the appsettings file.");
            }
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new Exception("A 'Api Key' configuration value is required in the appsettings file.");
            }
            if (string.IsNullOrWhiteSpace(ApiHost))
            {
                throw new Exception("An 'Api Host' configuration value is required in the appsettings file.");
            }
            return new Client(AppId, ApiKey, ApiHost);
        }
    }

    
}
