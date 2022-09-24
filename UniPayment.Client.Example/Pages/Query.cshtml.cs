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
        private bool _isSandbox = false;
        public QueryModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _appId = configuration.GetValue<string>("AppId");
            _apiKey = configuration.GetValue<string>("ApiKey");
            _isSandbox = configuration.GetValue<bool>("isSandbox");
        }

        [BindProperty]
        public QueryInvoiceViewModel QueryRequest { get; set; }

        /// <summary>
        /// App
        /// </summary>
        [BindProperty]
        public ClientModel Client { get; set; }


        public Response<QueryResult<InvoiceModel>> QueryResponse { get; set; }

        public void OnGet()
        {

            this.Client = new ClientModel();
            this.Client.ClientId = this._appId;
            this.Client.ClientSecret = this._apiKey;


            this.QueryRequest = new QueryInvoiceViewModel();
            this.QueryRequest.PageNo = 1;
            this.QueryRequest.PageSize = 10;

        }


        public async Task<IActionResult> OnPostAsync()
        {
            var provider = new UniPaymentClientProvider(this.Client.ClientId, this.Client.ClientSecret, this._isSandbox);

            //Create UniPayment Client
            var client = provider.GetUniPaymentClient();

            try
            {
                //Send request to api
                this.QueryResponse = await client.QueryInvoiceAsync(this.QueryRequest);
            }
            catch (UniPaymentException ex)
            {
                this.QueryResponse = new Response<QueryResult<InvoiceModel>>
                {
                    Code = ex.Code,
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
