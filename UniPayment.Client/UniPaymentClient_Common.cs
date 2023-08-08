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
        public Response<string[]> GetIps()
        {
            return this.GetIpsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Query IP Of Notification Server
        /// </summary>
        /// <returns></returns>
        public Task<Response<string[]>> GetIpsAsync()
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            return this.Get<Response<string[]>>("/v1.0/ips", paramBuilder);
        }

        public Response<CurrencyModel[]> GetCurrencies()
        {
            return this.GetCurrenciesAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Query Currency
        /// </summary>
        /// <returns></returns>
        public Task<Response<CurrencyModel[]>> GetCurrenciesAsync()
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");
            return this.Get<Response<CurrencyModel[]>>("/v1.0/currencies", paramBuilder, false);
        }

        public Response<ExchangeRateModel[]> GetExchangeRates(string fiatCurrency = "USD")
        {
            return this.GetExchangeRatesAsync(fiatCurrency).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Query  ExchangeRate List Of FiatCurrency
        /// </summary>
        /// <param name="fiatCurrency"></param>
        /// <returns></returns>
        public Task<Response<ExchangeRateModel[]>> GetExchangeRatesAsync(string fiatCurrency = "USD")
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/rates/{fiatCurrency}";
            return this.Get<Response<ExchangeRateModel[]>>(action, paramBuilder, false);
        }

        public Response<ExchangeRateModel> GetExchangeRate(string fiatCurrency, string cryptoCurrency)
        {
            return this.GetExchangeRateAsync(fiatCurrency, cryptoCurrency).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Query ExchangeRate Of Crypto_Fiat Pair
        /// </summary>
        /// <param name="cryptoCurrency"></param>
        /// <param name="fiatCurrency"></param>
        /// <returns></returns>
        public Task<Response<ExchangeRateModel>> GetExchangeRateAsync(string fiatCurrency, string cryptoCurrency)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/rates/{fiatCurrency}/{cryptoCurrency}";
            return this.Get<Response<ExchangeRateModel>>(action, paramBuilder, false);
        }
    }
}