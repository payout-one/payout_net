using Payout.Lib.Base;
using System.Net.Http;

namespace Payout.Lib.Requests
{
    public class GetCheckoutRequest : BaseRequest
    {
        public long Id { get; set; }

        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"https://{host}/api/v1/checkouts/{this.Id}");
        }
    }
}