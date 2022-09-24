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
        var clientId = "06c6f5e9-8993-49d7-8457-ae894e670391";
        var clientSecret = "96YW8gmjzYkLP613NstqKt6GmZgC5cmLg";

        _client = new UniPaymentClient(clientId, clientSecret, true);
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Test_CreateInvoice()
    {
        var request = new CreateInvoiceRequest();
        request.AppId = "d186fa61-729e-46d3-b52c-8f3f355f032a";
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
        var response = _client.GetInvoiceById("DajcJHbCVGTpKqd6oBdGKQ");
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
        var notify =
            "{\"ipn_type\":\"invoice\",\"event\":\"invoice_created\",\"app_id\":\"d186fa61-729e-46d3-b52c-8f3f355f032a\",\"invoice_id\":\"DniLiQreepEYePhcwBZpno\",\"order_id\":\"2\",\"price_amount\":2.0,\"price_currency\":\"AUD\",\"network\":null,\"address\":null,\"pay_currency\":null,\"pay_amount\":0.0,\"exchange_rate\":0.0,\"paid_amount\":0.0,\"confirmed_amount\":0.0,\"refunded_price_amount\":0.0,\"create_time\":\"2022-09-24T09:56:26.0729376Z\",\"expiration_time\":\"2022-09-25T09:56:26.0729864Z\",\"status\":\"New\",\"error_status\":\"None\",\"ext_args\":null,\"transactions\":null,\"notify_id\":\"65aa67ab-abcb-4d94-a929-ac3552ad2296\",\"notify_time\":\"0001-01-01T00:00:00\"}";
        var response = _client.CheckIPN(notify);
        Assert.Equal("OK",response.Code);
        _testOutputHelper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
}