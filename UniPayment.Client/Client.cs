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
    
    public class Client
    {
        public const string UNIPAYMENT_SDK_CSHARP = "unipayment_sdk_netcore";

        public string AppId { get; set; }

        public string ApiKey { get; set; }

        public string ApiHost { get; set; }

        internal HttpClient HttpClient { get; set; }

        const string API_ORDER = "/v1.0/invoices";

        const string API_CURRNECY = "/v1.0/currencies";

        const string API_Rate = "/v1.0/rates";

        const string API_IP = "/v1.0/ips";


        public Client(string appId, string apiKey,string apiHost)
        {
            Utils.ValidateRequiredParameters(appId, apiKey, apiHost);

            this.AppId = appId;
            this.ApiKey = apiKey;
            this.ApiHost = apiHost;

            this.HttpClient = new HttpClient();
            AddUserAgent(this.HttpClient);
        }


        /// <summary>
        /// Create Invocie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Response<InvoiceModel>> CreateInvoice(CreateInvoiceRequest request)
        {
            return this.Post<Response<InvoiceModel>>(API_ORDER, request);
        }

        /// <summary>
        /// Query Invoice
        /// </summary>
        /// <param name="invoice_id"></param>
        /// <param name="order_id"></param>
        /// <param name="status"></param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <param name="is_asc"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<Response<QueryResult<InvoiceModel>>> QueryInvoice(QueryInvoiceRequest query)
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

            return this.Get<Response<QueryResult<InvoiceModel>>>(API_ORDER, paramBuilder);
        }

        public Task<Response<InvoiceDetailModel>> GetInvoice(string invoiceId)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"{API_ORDER}/{invoiceId}";
            return this.Get<Response<InvoiceDetailModel>>(action, paramBuilder);
        }

            /// <summary>
            /// Query IP Of Notification Server
            /// </summary>
            /// <returns></returns>
            public Task<Response<string[]>> GetIP()
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            return this.Get<Response<string[]>>(API_IP, paramBuilder);
        }

        /// <summary>
        /// Query Currency
        /// </summary>
        /// <returns></returns>
        public Task<Response<CurrencyModel[]>> GetCurrency()
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");
            return this.Get<Response<CurrencyModel[]>>(API_CURRNECY, paramBuilder);
        }


        /// <summary>
        /// Query  ExchangeRate List Of FiatCurrency
        /// </summary>
        /// <param name="fiatCurrency"></param>
        /// <returns></returns>
        public Task<Response<ExchangeRateModel[]>> GetRate(string fiatCurrency="USD")
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"{API_Rate}/{fiatCurrency}";
            return this.Get<Response<ExchangeRateModel[]>>(action, paramBuilder);
        }

        /// <summary>
        /// Query ExchangeRate Of Crypto_Fiat Pair
        /// </summary>
        /// <param name="cryptoCurrency"></param>
        /// <param name="fiatCurrency"></param>
        /// <returns></returns>
        public Task<Response<ExchangeRateModel>> GetRate(string fiatCurrency,string cryptoCurrency)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"{API_Rate}/{fiatCurrency}/{cryptoCurrency}";
            return this.Get<Response<ExchangeRateModel>>(action, paramBuilder);
        }




        internal async Task<T> Post<T>(string action, object obj)
        {
            var url = this.BuildUrl(action);

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url, UriKind.Absolute));

            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            await AddHMACAuthHeader(request);

            HttpResponseMessage httpResponse = await this.HttpClient.SendAsync(request);

            // This will throw an HttpRequestException if the result code is not in the 200s
            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);

        }

        internal async Task<T> Get<T>(string action, StringBuilder paramBuilder)
        {
            var url = this.BuildUrl(action,paramBuilder);

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url, UriKind.Absolute));

            await AddHMACAuthHeader(request);

            HttpResponseMessage httpResponse = await this.HttpClient.SendAsync(request);

            // This will throw an HttpRequestException if the result code is not in the 200s
            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);

        }



        private string BuildUrl(string action, StringBuilder paramBuilder=null)
        {
            if (paramBuilder == null)
            {
                return $"{this.ApiHost}{action}";
            }
            else
            {
                return $"{this.ApiHost}{action}?{paramBuilder.ToString()}";
            }
        }


        private  async Task AddHMACAuthHeader(HttpRequestMessage request)
        { 
            var requestContentBase64String = string.Empty;

            //1. RequestHttpMethod
            var requestHttpMethod = request.Method.Method;

            //2. RequestUri
            var requestUri = WebUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());

            //3. RequestTimeStamp (Calculate UNIX time)
            var requestTimeStamp = Utils.GetTimeStamp();

            //4. Nonce (Create random nonce for each request)
            var nonce = Utils.GetNonce();

            //5. RequestContentBase64String (Checking if the request contains body, usually will be null wiht HTTP GET and DELETE)
            if (request.Content != null)
            {
                var content = await request.Content.ReadAsByteArrayAsync();
                requestContentBase64String = Utils.GetContentBase64String(content);
            }

            //Get HMAC signature value
            var hmac = Utils.Sign(this.AppId, this.ApiKey, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

            //Append Header to request
            request.Headers.Authorization = new AuthenticationHeaderValue("Hmac", hmac);
        }


        private static void AddUserAgent(HttpClient httpClient)
        {
            // Product name and version
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ProductInfoHeaderValue ua = new ProductInfoHeaderValue(UNIPAYMENT_SDK_CSHARP, version);
            httpClient.DefaultRequestHeaders.UserAgent.Add(ua);

            // Additional info
            var os = Environment.OSVersion.ToString();
            ProductInfoHeaderValue stuff = new ProductInfoHeaderValue($"({os})");
            httpClient.DefaultRequestHeaders.UserAgent.Add(stuff);
        }
    }
}
