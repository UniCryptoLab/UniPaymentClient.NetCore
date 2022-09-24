using System;
using System.Text;
using System.Security.Cryptography;

namespace UniPayment.Client
{
    internal class AuthHelper
    {
        static DateTime Epoch = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);


        internal static string GetNonce()
        {
            return Guid.NewGuid().ToString("N"); ;
        }


        internal static ulong GetTimeStamp()
        {
            var timeSpan = DateTime.UtcNow - Epoch;
            return Convert.ToUInt64(timeSpan.TotalSeconds);
        }

        internal static string GetContentBase64String(byte[] content)
        {
            var md5 = MD5.Create();
            // Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
            var requestContentHash = md5.ComputeHash(content);
            return Convert.ToBase64String(requestContentHash);
        }

        internal static string GetRawData(string appId,string requestHttpMethod,string requestUri,ulong requestTimeStamp, string nonce,string requestContentBase64String)
        {
            return $"{appId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}{requestContentBase64String}";
        }

        internal static string Sign(string appId, string apiKey, string requestHttpMethod, string requestUri, ulong requestTimeStamp, string nonce, string requestContentBase64String)
        {
            var signatureRawData = $"{appId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}{requestContentBase64String}";

            var apiKeyBytes = Encoding.UTF8.GetBytes(apiKey);
            var signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (var hmac = new HMACSHA256(apiKeyBytes))
            {
                var signatureBytes = hmac.ComputeHash(signature);
                var requestSignatureBase64String = Convert.ToBase64String(signatureBytes);

                // Setting the values in the Authorization header using custom scheme (Hmac)
                return $"{appId}:{requestSignatureBase64String}:{nonce}:{requestTimeStamp}";
            }
        }
    }
}
