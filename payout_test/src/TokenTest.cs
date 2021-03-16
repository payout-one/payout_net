using Payout.Lib;
using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using Xunit;


namespace payout_tests
{
    public class TokenTest
    {
        [Fact]
        public async void GetTokenStatus()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.GetTokenStatus(new GetTokenStatusRequest { Token = "" });

            Assert.True(response != null);
        }

        [Fact]
        public async void DeleteToken()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var response = await clientService.DeleteToken(new DeleteTokenRequest { Token = "" });

            Assert.True(response != null);
        }
    }
}
