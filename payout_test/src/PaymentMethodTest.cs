using Payout.Lib;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using Payout.Lib.Models;
using System;
using Xunit;
using System.Text.Json;


namespace payout_tests
{
    public class PaymentMethodTest
    {

        [Fact]
        public async void GetPaymentsMethods()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.GetPaymentMethods(new GetPaymentMethodsRequest());


            Console.WriteLine(JsonSerializer.Serialize(response));

            Assert.True(response != null);
        }
    }
}
