using Payout.Lib;
using Payout.Lib.Requests;
using Payout.Lib.Responses;
using Payout.Lib.Services;
using Payout.Lib.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace payout_tests
{
    public class CheckoutTest
    {
        [Fact]
        public async void GetCheckout()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.GetCheckout(new GetCheckoutRequest { Id = 477815 });
            Console.WriteLine(JsonSerializer.Serialize(response));
            Assert.True(response != null);
        }

        [Fact]
        public async void CreateCheckout()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

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
                RedirectUrl = "https://test.host.sandbox/payout/redirect",
                ExternalId = Guid.NewGuid().ToString()
            });

            Console.WriteLine(JsonSerializer.Serialize(response));

            Assert.True(response != null);
        }
    }
}
