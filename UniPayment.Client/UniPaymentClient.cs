using System;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

using UniPayment.Client.Models;

namespace UniPayment.Client
{
    
    public class UniPaymentClient
    {
        string AppId { get; set; }
        string ApiKey { get; set; }
        string ApiHost { get; set; }
        HttpClient _httpClient { get; set; }
        
        public UniPaymentClient(string appId, string apiKey, bool isSandbox=false)
        {
            this.AppId = appId;
            this.ApiKey = apiKey;
            if (isSandbox)
            {
                this.ApiHost = "https://sandbox-api.unipayment.io";
            }
            else
            {
                this.ApiHost = "https://api.unipayment.io";
            }

            this._httpClient = new HttpClient();
            AddUserAgent(this._httpClient);
        }


        public Response<InvoiceModel> CreateInvoice(CreateInvoiceRequest request)
        {
            return this.CreateInvoiceAsync(request).GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Create Invoice
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Response<InvoiceModel>> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            return this.Post<Response<InvoiceModel>>("/v1.0/invoices", JsonConvert.SerializeObject(request));
        }

        public Response<QueryResult<InvoiceModel>> QueryInvoice(QueryInvoiceRequest query)
        {
            return this.QueryInvoiceAsync(query).GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Query Invoice
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<Response<QueryResult<InvoiceModel>>> QueryInvoiceAsync(QueryInvoiceRequest query)
        {
            StringBuilder paramBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(query.InvoiceId))
                paramBuilder.AppendFormat("invoice_id={0}&", query.InvoiceId);

            if(!string.IsNullOrEmpty(query.OrderId))
                paramBuilder.AppendFormat("order_id={0}&", query.OrderId);

            if (query.Status != null)
                paramBuilder.AppendFormat("status={0}&", query.Status);

            if (!query.PageNo.HasValue)
                paramBuilder.AppendFormat("page_no={0}&", 1);
            else
                paramBuilder.AppendFormat("page_no={0}&", query.PageNo);

            if (!query.PageSize.HasValue)
                paramBuilder.AppendFormat("page_size={0}&", 10);
            else
                paramBuilder.AppendFormat("page_size={0}&", query.PageSize.Value);

            paramBuilder.AppendFormat("is_asc={0}&", query.IsAsc);

            if(query.Start.HasValue)
                paramBuilder.AppendFormat("start={0}&", query.Start.Value);

            if (query.End.HasValue)
                paramBuilder.AppendFormat("end={0}&", query.End.Value);


            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            return this.Get<Response<QueryResult<InvoiceModel>>>("/v1.0/invoices", paramBuilder);
        }

        public Response<InvoiceDetailModel> GetInvoiceById(string invoiceId)
        {
            return this.GetInvoiceByIdAsync(invoiceId).GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Get Invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Task<Response<InvoiceDetailModel>> GetInvoiceByIdAsync(string invoiceId)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/invoices/{invoiceId}";
            return this.Get<Response<InvoiceDetailModel>>(action, paramBuilder);
        }

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
            return this.Get<Response<CurrencyModel[]>>("/v1.0/currencies", paramBuilder,false);
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
        public Task<Response<ExchangeRateModel[]>> GetExchangeRatesAsync(string fiatCurrency="USD")
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/rates/{fiatCurrency}";
            return this.Get<Response<ExchangeRateModel[]>>(action, paramBuilder,false);
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
        public Task<Response<ExchangeRateModel>> GetExchangeRateAsync(string fiatCurrency,string cryptoCurrency)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/rates/{fiatCurrency}/{cryptoCurrency}";
            return this.Get<Response<ExchangeRateModel>>(action, paramBuilder,false);
        }

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
        
        

        #region GET POST PUT DELETE
        private string BuildUrl(string path, StringBuilder paramBuilder=null)
        {
            if (paramBuilder == null)
            {
                return $"{this.ApiHost}{path}";
            }
            else
            {
                return $"{this.ApiHost}{path}?{paramBuilder.ToString()}";
            }
        }

        async Task<T> Get<T>(string path, StringBuilder parameters = null,
            bool signatureRequired = true)
        {
            try
            {
                var httpResponse = await Get(path, parameters, signatureRequired);
                httpResponse.EnsureSuccessStatusCode();
                var content = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                throw new UniPaymentApiException(e);
            }
        }
        
        
        /// <summary>
        /// Make a GET request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parameters"></param>
        /// <param name="signatureRequired"></param>
        /// <returns></returns>
        /// <exception cref="UniPaymentApiException"></exception>
        private async Task<HttpResponseMessage> Get(string path, StringBuilder parameters = null,
            bool signatureRequired = true)
        {
            try
            {
                var url = this.BuildUrl(path, parameters);
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url, UriKind.Absolute));
                if (signatureRequired)
                {
                    await SignRequest(request);
                }

                var result = await _httpClient.SendAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                throw new UniPaymentApiException(ex);
            }
        }
        
        async Task<T> Post<T>(string path,string json, bool signatureRequired = true)
        {
            try
            {
                var httpResponse = await Post(path, json, signatureRequired);
                httpResponse.EnsureSuccessStatusCode();
                var content = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                throw new UniPaymentApiException(e);
            }
        }
        
        private async Task<HttpResponseMessage> Post(string path, string json, bool signatureRequired = false)
        {
            try
            {
                var url = this.BuildUrl(path);
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url, UriKind.Absolute));
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                
                if (signatureRequired)
                {
                    await SignRequest(request);
                }

                var result = await _httpClient.SendAsync(request).ConfigureAwait(false);;
                return result;
            }
            catch (Exception ex)
            {
                throw new UniPaymentApiException(ex);
            }
        }
        

        #endregion

        private async Task SignRequest(HttpRequestMessage request)
        { 
            var requestContentBase64String = string.Empty;

            //1. RequestHttpMethod
            var requestHttpMethod = request.Method.Method;

            //2. RequestUri
            var requestUri = WebUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());

            //3. RequestTimeStamp (Calculate UNIX time)
            var requestTimeStamp = AuthHelper.GetTimeStamp();

            //4. Nonce (Create random nonce for each request)
            var nonce = AuthHelper.GetNonce();

            //5. RequestContentBase64String (Checking if the request contains body, usually will be null wiht HTTP GET and DELETE)
            if (request.Content != null)
            {
                var content = await request.Content.ReadAsByteArrayAsync();
                requestContentBase64String = AuthHelper.GetContentBase64String(content);
            }

            //Get HMAC signature value
            var hmac = AuthHelper.Sign(this.AppId, this.ApiKey, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

            //Append Header to request
            request.Headers.Authorization = new AuthenticationHeaderValue("Hmac", hmac);
        }


        private static void AddUserAgent(HttpClient httpClient)
        {
            // Product name and version
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ProductInfoHeaderValue ua = new ProductInfoHeaderValue("unipayment_sdk_netcore", version);
            httpClient.DefaultRequestHeaders.UserAgent.Add(ua);

            // Additional info
            var os = Environment.OSVersion.ToString();
            ProductInfoHeaderValue stuff = new ProductInfoHeaderValue($"({os})");
            httpClient.DefaultRequestHeaders.UserAgent.Add(stuff);
        }
    }
}
