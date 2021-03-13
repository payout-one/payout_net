using Payout.Lib.Requests;
using Payout.Lib.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payout.Lib.Interfaces
{
    public interface IPayoutClient
    {
        Task<AuthResponse> GetToken();
        Task<AuthResponse> GetCachedToken();
        Task<CheckoutResponse> GetCheckout(GetCheckoutRequest request);
        Task<CheckoutResponse> CreateCheckout(CreateCheckoutRequest request);
        Task<List<CheckoutResponse>> GetCheckouts(GetCheckoutsRequest request);
        Task<GetTokenStatusResponse> GetTokenStatus(GetTokenStatusRequest request);
        Task<DeleteTokenResponse> DeleteToken(DeleteTokenRequest request);
        Task<WithdrawalResponse> CreateWithdrawal(CreateWithdrawalRequest request);
        Task<WithdrawalResponse> GetWithdrawal(GetWithdrawalRequest request);
        Task<List<WithdrawalResponse>> GetWithdrawals(GetWithdrawalsRequest request);
        Task<RefundPaymentResponse> RefundPayment(RefundPaymentRequest request);
        Task<List<GetPaymentMethodsResponse>> GetPaymentMethods(GetPaymentMethodsRequest request);
        Task<List<GetBalanceResponse>> GetBalance(GetBalanceRequest request);
    }
}