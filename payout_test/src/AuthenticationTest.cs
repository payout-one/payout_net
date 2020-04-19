using Payout.Lib;
using Payout.Lib.Services;
using Payout.Lib.Models;
using System;
using System.Net.Http;
using Xunit;

namespace payout_tests
{
    public class AuthenticationTest
    {
        [Fact]
        public async void AuthenticateEmpty()
        {
            var apiKey = new ApiKey { Key = "", Secret = "", Host = Constants.HOST };
            var clientService = new ClientService(apiKey);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(clientService.GetToken);
        }

        [Fact]
        public async void AuthenticateApiKey()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });

            var token = await clientService.GetToken();

            Assert.True(token != null);
        }
    }
}
