using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniPayment.Client.Models;

namespace UniPayment.Client.Example.Pages
{
    [BindProperties]
    public class QueryInvoiceViewModel : QueryInvoiceRequest
    {

    }

    public class QueryModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private string _appId = string.Empty;
        private string _apiKey = string.Empty;
        private string _apiHost = string.Empty;
        public QueryModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _appId = configuration.GetValue<string>("AppId");
            _apiKey = configuration.GetValue<string>("ApiKey");
            _apiHost = configuration.GetValue<string>("ApiHost");
        }

        [BindProperty]
        public QueryInvoiceViewModel QueryRequest { get; set; }

        /// <summary>
        /// App
        /// </summary>
        [BindProperty]
        public AppModel App { get; set; }


        public Response<QueryResult<InvoiceModel>> QueryResponse { get; set; }

        public void OnGet()
        {

            this.App = new AppModel();
            this.App.ApiHost = this._apiHost;
            this.App.AppId = this._appId;
            this.App.ApiKey = this._apiKey;


            this.QueryRequest = new QueryInvoiceViewModel();
            this.QueryRequest.PageNo = 1;
            this.QueryRequest.PageSize = 10;

        }


        public async Task<IActionResult> OnPostAsync()
        {
            var provider = new UniPaymentClientProvider(this.App.AppId, this.App.ApiKey, this.App.ApiHost);

            //Create UniPayment Client
            var client = provider.GetUniPaymentClient();

            try
            {
                //Send request to api
                this.QueryResponse = await client.QueryInvoice(this.QueryRequest);
            }
            catch (HttpRequestException ex)
            {
                this.QueryResponse = new Response<QueryResult<InvoiceModel>>
                {
                    Code = "HttpRequestException",
                    Msg = ex.Message,
                };
            }
            catch (Exception ex)
            {
                this.QueryResponse = new Response<QueryResult<InvoiceModel>>
                {
                    Code = "Exception",
                    Msg = ex.Message,
                };
            }

            return Page();
        }
    }
}
