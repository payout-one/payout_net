using Payout.Lib.Base;
using System.Net.Http;

namespace Payout.Lib.Requests
{
    public class GetWithdrawalListRequest : BaseListRequest
    {
        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"https://{host}/api/v1/withdrawals?limit={this.Limit}&offset={this.Offset}");
        }
    }
}
