using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using System.Threading.Tasks;

namespace ConsoleApp48
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var key = "";
            var secret = "";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            var checkouts = await clientService.GetCheckouts(new GetCheckoutsRequest { Limit = 50, Offset = 0 });
        }
    }
}