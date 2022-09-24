using Xunit;
using UniPayment.Client;
using UniPayment.Client.Models;
using Xunit.Abstractions;

namespace Unipayment.Test;

public class UnitTest1
{
    private UniPaymentClient _client;
    private ITestOutputHelper _testOutputHelper;
    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        var appId = "cee1b9e2-d90c-4b63-9824-d621edb38012";
        var apiKey = "9G62Fd7fCQGyznVvatk4SAfGsHDEt819E";

        _client = new UniPaymentClient(appId, apiKey, true);
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Test_CreateInvoice()
    {
        var request = new CreateInvoiceRequest();
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
        var response = _client.GetInvoiceById("9EfHVGLDjQssJv7xnBsDSM");
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
        var notify = "{\"ipn_type\":\"invoice\",\"event\":\"invoice_created\",\"app_id\":\"cee1b9e2-d90c-4b63-9824-d621edb38012\",\"invoice_id\":\"12wQquUmeCPUx3qmp3aHnd\",\"order_id\":\"ORDER_123456\",\"price_amount\":2.0,\"price_currency\":\"USD\",\"network\":null,\"address\":null,\"pay_currency\":null,\"pay_amount\":0.0,\"exchange_rate\":0.0,\"paid_amount\":0.0,\"confirmed_amount\":0.0,\"refunded_price_amount\":0.0,\"create_time\":\"2022-09-14T04:57:54.5599307Z\",\"expiration_time\":\"2022-09-14T05:02:54.559933Z\",\"status\":\"New\",\"error_status\":\"None\",\"ext_args\":\"Merchant Pass Through Data\",\"transactions\":null,\"notify_id\":\"fd58cedd-67c6-4053-ae65-2f6fb09a7d2c\",\"notify_time\":\"0001-01-01T00:00:00\"}";
        var response = _client.CheckIPN(notify);
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
}