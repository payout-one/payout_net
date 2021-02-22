using System.Net.Http;
using Payout.Lib.Base;

namespace Payout.Lib.Requests
{
   public class GetCheckoutsRequest : BaseRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"https://{host}/api/v1/checkouts?limit={this.Limit}&offset={this.Offset}");
        }
    }
}
