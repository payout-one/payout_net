using Payout.Lib;
using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using System;
using System.Linq;
using Xunit;

namespace payout_tests
{
    public class WithdrawalTest
    {
        [Fact]
        public async void GetWithdrawal()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            var response = await clientService.GetWithdrawal(new GetWithdrawalRequest { Id = 145 });

            Assert.True(response != null);
            Assert.True(response.Signature == response.CalculateSignature(signatureService));
        }

        [Fact]
        public async void GetWithdrawals()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            var response = await clientService.GetWithdrawals(new GetWithdrawalListRequest { Limit = 50, Offset = 0 });

            Assert.True(response != null);
            Assert.True(response.All(a => a.Signature == a.CalculateSignature(signatureService)));
        }

        [Fact]
        public async void CreateWithdrawals()
        {
            var clientService = new ClientService(new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET, Host = Constants.HOST });
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            var response = await clientService.CreateWithdrawal(new CreateWithdrawalRequest
            {
                Amount = 9,

                Currency = "EUR",
                Customer = new Customer
                {
                    Email = "test@test.host.sandbox",
                    FirstName = "Janko",
                    LastName = "Pokusny",
                    Phone = "421-999999999"
                },
                ExternalId = Guid.NewGuid().ToString(),
                IdempotencyKey = Guid.NewGuid().ToString(),
                Nonce = Guid.NewGuid().ToString(),
                Iban = "SK6807200002891987426353"
            });

            Assert.True(response != null);
            Assert.True(response.Signature == response.CalculateSignature(signatureService));
        }
    }
}