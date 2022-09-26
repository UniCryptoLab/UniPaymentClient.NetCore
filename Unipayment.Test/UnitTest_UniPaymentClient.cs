using Xunit;
using UniPayment.Client;
using UniPayment.Client.Models;
using Xunit.Abstractions;

namespace Unipayment.Test;

public class UnitTest1
{
    private UniPaymentClient _client;
    private ITestOutputHelper _testOutputHelper;
    
    private string _clientId = "74feb539-ba5a-4ae9-b901-4da4fb539574";
    private string _clientSecret = "BsoRhgqzhR1TYMtwTRYdPxBTvR5rxkW9K";
    private string _appId = "2a9bd90b-fe95-4659-83cb-04de662fbbac";
    private bool _isSandbox = true;

    private string _ipnNotify = "{\"ipn_type\":\"invoice\",\"event\":\"invoice_created\",\"app_id\":\"2a9bd90b-fe95-4659-83cb-04de662fbbac\",\"invoice_id\":\"SrAARgNrPgvveiBQtNc4gk\",\"order_id\":\"6330f1f118df1\",\"price_amount\":100.0,\"price_currency\":\"USD\",\"network\":null,\"address\":null,\"pay_currency\":\"USDT\",\"pay_amount\":0.0,\"exchange_rate\":0.0,\"paid_amount\":0.0,\"confirmed_amount\":0.0,\"refunded_price_amount\":0.0,\"create_time\":\"2022-09-26T00:27:29.6697063Z\",\"expiration_time\":\"2022-09-26T00:32:29.6698139Z\",\"status\":\"New\",\"error_status\":\"None\",\"ext_args\":null,\"transactions\":null,\"notify_id\":\"0443e623-492a-474a-bd22-b866d6b7beb9\",\"notify_time\":\"0001-01-01T00:00:00\"}";
    private string _invoiceID = "SrAARgNrPgvveiBQtNc4gk";
    
    
    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _client = new UniPaymentClient(_clientId, _clientSecret, _isSandbox);
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Test_CreateInvoice()
    {
        var request = new CreateInvoiceRequest();
        request.AppId = _appId;
        request.PriceAmount = 2.00f;
        request.PriceCurrency = "USD";
        request.NotifyURL = "https://demo-payment.requestcatcher.com/test";
        request.RedirectURL = "https://www.example.com";
        request.OrderId = "ORDER_123456";
        request.Title = "MacBook Pro";
        request.Description = "MacBook Pro(256G)";
        request.Lang = "en-US";
        request.ExtArgs = "Merchant Pass Through Data";
        request.ConfirmSpeed = "Medium";

        var response = _client.CreateInvoice(request);
        
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }

    [Fact]
    public void Test_QueryInvoice()
    {
        var request = new QueryInvoiceRequest();
        request.PageSize = 10;
        request.PageNo = 1;

        var response = _client.QueryInvoice(request);
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }

    [Fact]
    public void Test_GetInvoice()
    {
        var response = _client.GetInvoiceById(_invoiceID);
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }

    [Fact]
    public void Test_GetExchangeRates()
    {
        var response = _client.GetExchangeRates("USD");
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }

    [Fact]
    public void Test_GetExchangeRate()
    {
        var response = _client.GetExchangeRate("USD","BTC");
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }

    [Fact]
    public void Test_GetIps()
    {
        var response = _client.GetIps();
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }

    [Fact]
    public void Test_GetCurrencies()
    {
        
        var response = _client.GetCurrencies();
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
    
    [Fact]
    public void Test_CheckIpn()
    {
        var response = _client.CheckIPN(_ipnNotify);
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
}