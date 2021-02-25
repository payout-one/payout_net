using System;
using Payout.Lib;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using Payout.Lib.Models;
using System;
using Xunit;
using System.Text.Json;


namespace payout_tests
{
    public class BalanceTest
    {

        [Fact]
        public async void GetBalance()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.GetBalance(new GetBalanceRequest());


            Console.WriteLine(JsonSerializer.Serialize(response));

            Assert.True(response != null);
        }

     
    }
}
