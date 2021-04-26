using Payout.Lib.Base;
using System.Net.Http;

namespace Payout.Lib.Requests
{
    public class DeleteTokenRequest : BaseRequest
    {
        public string Token { get; set; }

        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Delete, $"https://{host}/api/v1/tokens/{this.Token}");
        }
    }
}