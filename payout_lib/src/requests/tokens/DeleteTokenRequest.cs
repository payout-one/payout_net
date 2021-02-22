using System.Net.Http;
using Payout.Lib.Base;

namespace Payout.Lib.Requests
{
    public class DeleteTokenRequest : BaseRequest
    {
        public string Token { get; set; }
        public override HttpRequestMessage Request(string host)
        {
            //https://app.payout.one/api/v1/tokens/:token_value/
            return new HttpRequestMessage(HttpMethod.Delete, $"https://{host}/api/v1/tokens/{this.Token}");
        }
    }
}

