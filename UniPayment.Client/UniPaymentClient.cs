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
    
    public partial class UniPaymentClient
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string ApiHost { get; set; }
        HttpClient _httpClient { get; set; }
        
        public UniPaymentClient(string clientId, string clientSecret, bool isSandbox=false)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
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
            var hmac = AuthHelper.Sign(this.ClientId, this.ClientSecret, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

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
