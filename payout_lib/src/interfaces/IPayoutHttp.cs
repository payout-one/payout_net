using System.Net.Http;
using System.Threading.Tasks;

namespace Payout.Lib.Interfaces
{
    public interface IPayoutHttp
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage _);
        Task<HttpResponseMessage> SendAuthenticatedAsync(HttpRequestMessage _, string __);
    }
}