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
        public Response<List<BalanceModel>> GetBalances()
        {
            return this.GetBalancesAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Create Withdrawal
        /// </summary>
        public Task<Response<List<BalanceModel>>> GetBalancesAsync()
        {
            return this.Get<Response<List<BalanceModel>>>("/v1.0/wallet/balances");
        }


        public Response<WithdrawalModel> CreateWithdrawal(CreateWithdrawalRequest request)
        {
            return this.CreateWithdrawalAsync(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Create Withdrawal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Response<WithdrawalModel>> CreateWithdrawalAsync(CreateWithdrawalRequest request)
        {
            return this.Post<Response<WithdrawalModel>>("/v1.0/wallet/withdrawals",
                JsonConvert.SerializeObject(request));
        }

        public Response<Response> CancelWithdrawal(CancelWithdrawalRequest request)
        {
            return this.CancelWithdrawalAsync(request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Cancel Withdrawal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Response<Response>> CancelWithdrawalAsync(CancelWithdrawalRequest request)
        {
            return this.Post<Response<Response>>("/v1.0/wallet/withdrawals/cancel",
                JsonConvert.SerializeObject(request));
        }

        public Response<WithdrawalModel> GetWithdrawalById(string withdrawalId)
        {
            return this.GetWithdrawalByIdAsync(withdrawalId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Withdrawal
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Task<Response<WithdrawalModel>> GetWithdrawalByIdAsync(string withdrawalId)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/wallet/withdrawals/{withdrawalId}";
            return this.Get<Response<WithdrawalModel>>(action, paramBuilder);
        }

        public Response<QueryResult<WithdrawalModel>> QueryWithdrawal(QueryWithdrawalRequest query)
        {
            return this.QueryWithdrawalAsync(query).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Query Invoice
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<Response<QueryResult<WithdrawalModel>>> QueryWithdrawalAsync(QueryWithdrawalRequest query)
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

            return this.Get<Response<QueryResult<WithdrawalModel>>>("/v1.0/wallet/withdrawals", paramBuilder);
        }
        
        public Response<List<WithdrawalFee>> GetWithdrawalFees(string assetType)
        {
            return this.GetWithdrawalFeesAsync(assetType).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Withdraw Fee
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Task<Response<List<WithdrawalFee>>> GetWithdrawalFeesAsync(string assetType)
        {
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append($"rd={DateTime.UtcNow:yyyyMMddHHmmssffff}");

            var action = $"/v1.0/wallet/withdrawal-fees/{assetType}";
            return this.Get<Response<List<WithdrawalFee>>>(action, paramBuilder);
        }
    }
}