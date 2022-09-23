using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using System.Text.Json.Serialization;
using UniPayment.Client;
using UniPayment.Client.Models;

namespace UniPayment.Client.Example.Pages
{
    [BindProperties]
    public class InvoiceViewModel: CreateInvoiceRequest
    {
       
    }

    [BindProperties]
    public class AppModel
    {
        public string AppId { get; set; }

        public string ApiKey { get; set; }

    }


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private string _appId = string.Empty;
        private string _apiKey = string.Empty;
        private string _storeId = string.Empty;
        private bool _isSandbox = false;
        public IndexModel(ILogger<IndexModel> logger,IConfiguration configuration)
        {
            _logger = logger;
            _appId = configuration.GetValue<string>("AppId");
            _apiKey = configuration.GetValue<string>("ApiKey");
            _storeId = configuration.GetValue<string>("StoreId");
            _isSandbox = configuration.GetValue<bool>("isSandbox");
        }

        /// <summary>
        /// Create Invoice Request
        /// </summary>
        [BindProperty]
        public InvoiceViewModel CreateInvoiceRequest { get; set; }


        /// <summary>
        /// App
        /// </summary>
        [BindProperty]
        public AppModel App { get; set; }



        /// <summary>
        /// Creaet Invoice Response
        /// </summary>
        public Response<InvoiceModel> CreateInvoiceResponse { get; set; }

        public void OnGet()
        {
            this.App = new AppModel();
            this.App.AppId = this._appId;
            this.App.ApiKey = this._apiKey;

            
            this.CreateInvoiceRequest = new InvoiceViewModel();
            this.CreateInvoiceRequest.StoreId = _storeId;
            this.CreateInvoiceRequest.PriceAmount = 2.00f;
            this.CreateInvoiceRequest.PriceCurrency = "USD";
            this.CreateInvoiceRequest.NotifyURL = "https://demo-payment.requestcatcher.com/test";
            this.CreateInvoiceRequest.RedirectURL = "https://www.example.com";
            this.CreateInvoiceRequest.OrderId = "ORDER_123456";
            this.CreateInvoiceRequest.Title = "MacBook Pro";
            this.CreateInvoiceRequest.Description = "MacBook Pro(256G)";
            this.CreateInvoiceRequest.Lang = "en-US";
            this.CreateInvoiceRequest.ExtArgs = "Merchant Pass Through Data";
            this.CreateInvoiceRequest.ConfirmSpeed = "Medium";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var provider = new UniPaymentClientProvider(this.App.AppId, this.App.ApiKey, this._isSandbox);

            //Create UniPayment Client
            var client = provider.GetUniPaymentClient();

            //var ip = await client.GetIP();
            //var currency = await client.GetCurrency();
            //var rate1 = await client.GetRate("USD");
            //var rate2 = await client.GetRate("USD", "BTC");
            //var invoice = await client.GetInvoice("SGQbfT3L7BgW7suDMZmud3");

            try
            {
                //Send request to api
                this.CreateInvoiceResponse = await client.CreateInvoiceAsync(this.CreateInvoiceRequest);
                if(this.CreateInvoiceResponse.Code =="OK")
                {
                    return new RedirectResult(url: this.CreateInvoiceResponse.Data.InvoiceUrl);
                }
            }
            catch (UniPaymentException ex)
            {
                this.CreateInvoiceResponse = new Response<InvoiceModel>()
                {
                    Code = ex.Code,
                    Msg = ex.Message,
                };
            }
            catch (Exception ex)
            {
                this.CreateInvoiceResponse = new Response<InvoiceModel>()
                {
                    Code = "Exception",
                    Msg = ex.Message,
                };
            }

            return Page();
        }




        
    }
}
