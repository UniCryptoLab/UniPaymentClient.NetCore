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
        public Response<PayoutDetailModel> CreatePayout(CreatePayoutRequest request)
        {
            return this.CreatePayoutAsync(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Create Payout
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Response<PayoutDetailModel>> CreatePayoutAsync(CreatePayoutRequest request)
        {
            return this.Post<Response<PayoutDetailModel>>("/v1.0/payouts", JsonConvert.SerializeObject(request));
        }

        public Response<PayoutDetailModel> GetPayoutById(string payoutId)
        {
            return this.GetPayoutByIdAsync(payoutId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Payout
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Task<Response<PayoutDetailModel>> GetPayoutByIdAsync(string payoutId)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/payouts/{payoutId}";
            return this.Get<Response<PayoutDetailModel>>(action, paramBuilder);
        }

        public Response<QueryResult<PayoutModel>> QueryPayout(QueryPayoutRequest query)
        {
            return this.QueryPayoutAsync(query).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Query Payout
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<Response<QueryResult<PayoutModel>>> QueryPayoutAsync(QueryPayoutRequest query)
        {
            StringBuilder paramBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.Network))
                paramBuilder.AppendFormat("network={0}&", query.Network);

            if (!string.IsNullOrEmpty(query.AssetType))
                paramBuilder.AppendFormat("asset_type={0}&", query.AssetType);

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

            if (query.Start.HasValue)
                paramBuilder.AppendFormat("start={0}&", query.Start.Value);

            if (query.End.HasValue)
                paramBuilder.AppendFormat("end={0}&", query.End.Value);


            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            return this.Get<Response<QueryResult<PayoutModel>>>("/v1.0/payouts", paramBuilder);
        }
    }
}