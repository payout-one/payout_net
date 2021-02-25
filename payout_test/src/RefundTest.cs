using Payout.Lib;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using Payout.Lib.Models;
using System;
using Xunit;
using System.Text.Json;


namespace payout_tests
{
    public class RefundTest
    {
     
        [Fact]
        public async void RefundPayment()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.RefundPayment(new RefundPaymentRequest
            {
                CheckoutId = 480060,
                Currency = "EUR",
                Amount = 25,
                ExternalId = "0b8ac510-4b6f-4926-9375-90b9485073f1",
                Iban = "SK6807200002891987426353",
                StatementDescriptor = "test",
                Nonce = "ZUc0Mk9sVXZDOXNsdklzMQ"
            });
        

            Console.WriteLine(JsonSerializer.Serialize(response));

            Assert.True(response != null);
        }
    }
}
