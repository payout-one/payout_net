using System.Net.Http;
using Payout.Lib.Base;

namespace Payout.Lib.Requests
{
    public class GetPaymentMethodsRequest : BaseRequest
    {
        public override HttpRequestMessage Request(string host)
        {
            //https://app.payout.one/api/v1/payment_methods
            return new HttpRequestMessage(HttpMethod.Get, $"https://{host}/api/v1/payment_methods");
        }
    }
}