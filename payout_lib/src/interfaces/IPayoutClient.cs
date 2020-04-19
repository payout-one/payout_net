using System.Threading.Tasks;
using Payout.Lib.Requests;
using Payout.Lib.Responses;

namespace Payout.Lib.Interfaces
{
    public interface IPayoutClient
    {
        Task<AuthResponse> GetToken();
        Task<AuthResponse> GetCachedToken();
        Task<CheckoutResponse> GetCheckout(GetCheckoutRequest request);
        Task<CheckoutResponse> CreateCheckout(CreateCheckoutRequest request);
    }
}