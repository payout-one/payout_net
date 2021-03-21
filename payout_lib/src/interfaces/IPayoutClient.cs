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
        Task<CheckoutListResponse> GetCheckouts(GetCheckoutListRequest request);
        Task<TokenStatusResponse> GetTokenStatus(GetTokenStatusRequest request);
        Task<DeleteTokenResponse> DeleteToken(DeleteTokenRequest request);
        Task<WithdrawalResponse> CreateWithdrawal(CreateWithdrawalRequest request);
        Task<WithdrawalResponse> GetWithdrawal(GetWithdrawalRequest request);
        Task<WithdrawalListResponse> GetWithdrawals(GetWithdrawalListRequest request);
        Task<RefundPaymentResponse> RefundPayment(RefundPaymentRequest request);
        Task<PaymentMethodListResponse> GetPaymentMethods(GetPaymentMethodsRequest request);
        Task<GetBalanceListResponse> GetBalance(GetBalanceRequest request);
    }
}