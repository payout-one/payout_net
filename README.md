GetCheckoutRequest
```
var clientService = new ClientService(new ApiKey { Key = "API_KEY", Secret = "API_SECRET", Host = "sandbox.payout.one" });

var response = await clientService.GetCheckout(new GetCheckoutRequest { Id = 477794 });
```

CheckoutResponse
```
{
    "amount": 1500,
    "currency": "EUR",
    "customer": {
        "first_name": "FirstNameTest",
        "last_name": "LastNameTest",
        "email": "test@test.host.sandbox"
    },
    "id": 477815,
    "metadata": null,
    "object": "checkout",
    "payment": null,
    "redirect_url": "https://test.host.sandbox/payout/redirect",
    "external_id": "d7bb1dbb-e517-4a42-95fa-9d116f12b141",
    "status": "processing",
    "nonce": "ekNrUGE4T3B0dmxiM3Mxbw",
    "signature": "698b0d4b2592581e56e9d42d05a2d73c41b87270d506093b920125c947dee002"
}
```

CreateCheckoutRequest
```
var clientService = new ClientService(new ApiKey { Key = "API_KEY", Secret = "API_SECRET", Host = "sandbox.payout.one" });

var response = await clientService.CreateCheckout(new CreateCheckoutRequest
{
    Amount = 1500,
    Currency = "EUR",
    Customer = new Customer
    {
        FirstName = "FirstNameTest",
        LastName = "LastNameTest",
        Email = "test@test.host.sandbox"
    },
    RedirectUrl = "https://test.host.sandbox/payout/api",
    ExternalId = Guid.NewGuid().ToString()
});
```

CheckoutResponse
```
{
    "amount": 1500,
    "currency": "EUR",
    "customer": {
        "first_name": "FirstNameTest",
        "last_name": "LastNameTest",
        "email": "test@test.host.sandbox"
    },
    "id": 477815,
    "metadata": null,
    "object": "checkout",
    "payment": null,
    "redirect_url": "https://test.host.sandbox/payout/redirect",
    "external_id": "d7bb1dbb-e517-4a42-95fa-9d116f12b141",
    "status": "processing",
    "nonce": "NHE3azVweWNXQ0dFcGpsaw",
    "signature": "872d46a2bba949041ce5340c9708b97a91dc3d7598246a2a6b649da064b1b4d3"
}
```


Notification
```
var signatureService = new SignatureService { ApiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET } };

string json = "{\"data\":{\"amount\":20078,\"currency\":\"EUR\",\"customer\":{\"email\":\"test@payout.one\",\"first_name\":\"Lukáš\",\"last_name\":\"Tester\"},\"external_id\":\"1390\",\"id\":477738,\"idempotency_key\":null,\"metadata\":null,\"object\":\"checkout\",\"payment\":null,\"redirect_url\":\"https://localhost\",\"status\":\"processing\"},\"external_id\":\"1390\",\"nonce\":\"TXRYWklDTjdCZHBQTno2Nw==\",\"object\":\"webhook\",\"signature\":\"47db1d8f17fb01b387fa42e3cfa0d3fba63b943f25bbbcec66d9242c17f7d9ec\",\"type\":\"bank_transfer.in_transit\"}";
var notification = JsonSerializer.Deserialize<BaseNotification>(json);

if (notification.Signature == notification.CalculateSignature(signatureService))
{
    var checkout = notification.TryParse<Checkout>();
    return checkout;
}

throw new Exception("Invalid signature");
```