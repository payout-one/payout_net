using Payout.Lib;
using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace payout_tests
{
    public class CheckoutTest
    {
        [Fact]
        public async void GetCheckout()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };


            var response = await clientService.GetCheckout(new GetCheckoutRequest { Id = 480047 });

            Assert.True(response != null);
            Assert.True(response.Signature == response.CalculateSignature(signatureService));

        }

        [Fact]
        public async void GetCheckouts()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            var response = await clientService.GetCheckouts(new GetCheckoutsRequest { Limit = 50, Offset = 0 });

            Assert.True(response != null);
            Assert.True(response.All(a => a.Signature == a.CalculateSignature(signatureService)));
        }

        [Fact]
        public async void CreateCheckout()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            List<Product> products = new List<Product>();

            var product1 = new Product { Name = "product 1", Quantity = 1, UnitPrice = 12 };
            var product2 = new Product { Name = "product 2", Quantity = 2, UnitPrice = 120 };

            products.Add(product1);
            products.Add(product2);

            var response = await clientService.CreateCheckout(new CreateCheckoutRequest
            {
                Amount = 99,
                BillingAddress = new Address
                {
                    AddressLine1 = "Floriánska 6",
                    City = "Kosice",
                    CountryCode = "SK",
                    Name = "Adresa 1",
                    PostalCode = "04001"
                },
                ShippingAddress = new Address
                {
                    AddressLine1 = "Floriánska 4",
                    City = "Kosice",
                    CountryCode = "SK",
                    Name = "Adresa 2",
                    PostalCode = "04001"
                },
                Currency = "EUR",
                Customer = new Customer
                {
                    FirstName = "FirstNameTest",
                    LastName = "LastNameTest",
                    Email = "test@test.host.sandbox",
                    Phone = "421-999999999"
                },
                ExternalId = Guid.NewGuid().ToString(),
                IdempotencyKey = Guid.NewGuid().ToString(),
                Nonce = Guid.NewGuid().ToString(),
                Products = products,
                RedirectUrl = "https://payout.one/payment/redirect",
            });

            Assert.True(response != null);
            Assert.True(response.Signature == response.CalculateSignature(signatureService));
        }
    }
}
