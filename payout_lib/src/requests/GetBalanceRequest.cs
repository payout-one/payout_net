using Payout.Lib.Base;
using System.Net.Http;

namespace Payout.Lib.Requests
{
    public class GetBalanceRequest : BaseRequest
    {
        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"https://{host}/api/v1/balance");
        }
    }
}