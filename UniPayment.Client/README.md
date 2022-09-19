# UniPayment .Net Client
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/UniCryptoLab/UniPaymentClient.Python/blob/main/UniPaymentClient/LICENSE.txt)
[![NuGet version (BitPay.Light)](https://img.shields.io/nuget/v/unipayment.client.svg?style=flat-square)](https://www.nuget.org/packages/UniPayment.Client/)

A .NET client for the [UniPayment Client API](https://unipayment.readme.io/reference/overview).  

This SDK provides a convenient abstraction of UniPayment's Gateway API and allows developers to focus on payment flow/e-commerce integration rather than on the specific details of client-server interaction using the raw API.


## Getting Started

Before using the UniPayment API, sign up for your [API key](https://console.unipayment.io/).

If you want to use the Sandbox, sign up [here](https://sandbox-console.unipayment.io/).

## Installation

### Manual
1. Download the package and extract it into a local directory or clone the repo.
2. Add reference from your project.


### Using Nuget
```bash
nuget install UniPayment.Client
```


## Initializing UniPayment client
```c#
using UniPayment.Client;

client = UniPaymentClient(AppId, ApiKey);
```

Sandbox is used in the same way with is_sandbox as true.

```c#
using UniPayment.Client;

client = UniPaymentClient(AppId, ApiKey);
```

## Create an invoice
> Reference：https://unipayment.readme.io/reference/create_invoice

```c#
using UniPayment.Client;
using UniPayment.Client.Models;

app_id='your app id'
api_key='your api key'

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
```
### CreateInvoiceResponse

```json
{
  "code": "OK",
  "msg": "",
  "data": {
    "app_id": "cee1b9e2-d90c-4b63-9824-d621edb38012",
    "invoice_id": "Dj2mNCXXWCGKT89kcU8NJn",
    "order_id": "ORDER_123456",
    "price_amount": 2.0,
    "price_currency": "USD",
    "network": null,
    "address": null,
    "pay_amount": 0.0,
    "pay_currency": null,
    "exchange_rate": 0.0,
    "paid_amount": 0.0,
    "create_time": "2022-09-14T06:31:57",
    "expiration_time": "2022-09-14T06:36:57",
    "confirm_speed": 2,
    "status": 1,
    "error_status": 0,
    "invoice_url": "https://sandbox-app.unipayment.io/i/Dj2mNCXXWCGKT89kcU8NJn"
  }
}

```

## Handle IPN
> Reference：https://unipayment.readme.io/reference/ipn-check

> Invoice Status: https://unipayment.readme.io/reference/invoice-status

IPNs (Instant Payment Notifications) are sent to the notify_url when order status is changed to paid, confirmed and complete. 

```c#

[ActionName("notify")]
public async Task<string> Post()
{
    var notify = await new StreamReader(Request.Body).ReadToEndAsync();

    var client = new UniPaymentClient(_appId, _apiKey, true);
    var result = await client.CheckIPNAsync(notify);
    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
}

```

IPN notify
``` json
{
	"ipn_type": "invoice",
	"event": "invoice_expired",
	"app_id": "cee1b9e2-d90c-4b63-9824-d621edb38012",
	"invoice_id": "3Q7fyLnB2YNhUDW1fFNyEz",
	"order_id": "20",
	"price_amount": 6.0,
	"price_currency": "SGD",
	"network": null,
	"address": null,
	"pay_currency": null,
	"pay_amount": 0.0,
	"exchange_rate": 0.0,
	"paid_amount": 0.0,
	"confirmed_amount": 0.0,
	"refunded_price_amount": 0.0,
	"create_time": "2022-09-12T03:36:03",
	"expiration_time": "2022-09-12T03:41:03",
	"status": "Expired",
	"error_status": "None",
	"ext_args": null,
	"transactions": null,
	"notify_id": "8ccd2b61-226b-48e5-99b8-acb1f350313e",
	"notify_time": "2022-09-12T03:56:10.5852752Z"
}
```

## Run Example

1.Get source code form GitHub 
``` bash
git clone https://github.com/UniCryptoLab/UniPaymentClient.NetCore.git
```

2.Build and run UniPayment.Client.Examle


## License

MIT License

Copyright (c) 2021 UniPayment

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.