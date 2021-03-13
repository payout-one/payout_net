using Payout.Lib;
using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using Xunit;


namespace payout_tests
{
    public class BalanceTest
    {

        [Fact]
        public async void GetBalance()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.GetBalance(new GetBalanceRequest());

            Assert.True(response != null);
        }


    }
}
