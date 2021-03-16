using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payout.Lib.Services
{
    public class ClientService : IPayoutClient
    {
        private readonly HttpClientHandler _clientHandler;
        private readonly SignatureService _signatureService;
        private ApiKey _apiKey;
        private AuthResponse _authentication;
        private DateTime _validMax;

        public ClientService(ApiKey apiKey)
        {
            this._clientHandler = new HttpClientHandler(); ;
            this._signatureService = new SignatureService { ApiKey = apiKey };
            this._apiKey = apiKey;
        }

        #region Auth
        public async Task<AuthResponse> GetToken()
        {
            var request = new AuthRequest { ApiKey = this._apiKey };
            var response = await this.SendAsync(request.Request(this._apiKey.Host));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                _authentication = JsonSerializer.Deserialize<AuthResponse>(body);

                _validMax = DateTime.Now.AddSeconds(_authentication.ValidFor);

                return _authentication;
            }

            throw new UnauthorizedAccessException(response.ToString());
        }
        public async Task<AuthResponse> GetCachedToken()
        {
            if (this._validMax < DateTime.Now)
            {
                await this.GetToken();
            }

            return this._authentication;
        }
        #endregion

        #region Token
        public async Task<GetTokenStatusResponse> GetTokenStatus(GetTokenStatusRequest request)
        {
            var token = await GetCachedToken();

            return await this.SendAuthenticatedAndReturnResultAsync<GetTokenStatusRequest, GetTokenStatusResponse>(request, request.Request(this._apiKey.Host), token.Token);
        }
        public async Task<DeleteTokenResponse> DeleteToken(DeleteTokenRequest request)
        {
            var token = await GetCachedToken();

            return await this.SendAuthenticatedAndReturnResultAsync<DeleteTokenRequest, DeleteTokenResponse>(request, request.Request(this._apiKey.Host), token.Token);
        }
        #endregion

        #region Checkout
        public async Task<CheckoutResponse> CreateCheckout(CreateCheckoutRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

            var checkout = await this.SendAuthenticatedAndReturnResultAsync<CreateCheckoutRequest, CheckoutResponse>(request, request.Request(this._apiKey.Host), token.Token);

            if (checkout.Signature == checkout.CalculateSignature(this._signatureService))
                return checkout;

            throw new Exception($"Signature error, response signature: {checkout.Signature}, calculated signature: {checkout.CalculateSignature(this._signatureService)}");
        }
        public async Task<CheckoutResponse> GetCheckout(GetCheckoutRequest request)
        {
            var token = await GetCachedToken();

            var checkout = await this.SendAuthenticatedAndReturnResultAsync<GetCheckoutRequest, CheckoutResponse>(request, request.Request(this._apiKey.Host), token.Token);

            if (checkout.Signature == checkout.CalculateSignature(this._signatureService))
                return checkout;

            throw new Exception($"Signature error, response signature: {checkout.Signature}, calculated signature: {checkout.CalculateSignature(this._signatureService)}");
        }
        public async Task<List<CheckoutResponse>> GetCheckouts(GetCheckoutsRequest request)
        {
            var token = await GetCachedToken();

            var checkouts = await this.SendAuthenticatedAndReturnResultAsync<GetCheckoutsRequest, List<CheckoutResponse>>(request, request.Request(this._apiKey.Host), token.Token);

            if (checkouts.All(a => a.Signature == a.CalculateSignature(this._signatureService)))
                return checkouts;

            throw new Exception($"Signature error.");
        }
        #endregion

        #region Withdrawals
        public async Task<WithdrawalResponse> CreateWithdrawal(CreateWithdrawalRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

            var withdrawal = await this.SendAuthenticatedAndReturnResultAsync<CreateWithdrawalRequest, WithdrawalResponse>(request, request.Request(this._apiKey.Host), token.Token);

            if (withdrawal.Signature == withdrawal.CalculateSignature(this._signatureService))
                return withdrawal;

            throw new Exception($"Signature error, response signature: {withdrawal.Signature}, calculated signature: {withdrawal.CalculateSignature(this._signatureService)}");
        }
        public async Task<WithdrawalResponse> GetWithdrawal(GetWithdrawalRequest request)
        {
            var token = await GetCachedToken();

            var withdrawal = await this.SendAuthenticatedAndReturnResultAsync<GetWithdrawalRequest, WithdrawalResponse>(request, request.Request(this._apiKey.Host), token.Token);

            if (withdrawal.Signature == withdrawal.CalculateSignature(this._signatureService))
                return withdrawal;

            throw new Exception($"Signature error, response signature: {withdrawal.Signature}, calculated signature: {withdrawal.CalculateSignature(this._signatureService)}");
        }
        public async Task<List<WithdrawalResponse>> GetWithdrawals(GetWithdrawalsRequest request)
        {
            var token = await GetCachedToken();

            var withdrawals = await this.SendAuthenticatedAndReturnResultAsync<GetWithdrawalsRequest, List<WithdrawalResponse>>(request, request.Request(this._apiKey.Host), token.Token);

            if (withdrawals.All(a => a.Signature == a.CalculateSignature(this._signatureService)))
                return withdrawals;

            throw new Exception($"Signature error.");
        }
        #endregion

        #region Refunds
        public async Task<RefundPaymentResponse> RefundPayment(RefundPaymentRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

            var refund = await this.SendAuthenticatedAndReturnResultAsync<RefundPaymentRequest, RefundPaymentResponse>(request, request.Request(this._apiKey.Host), token.Token);

            if (refund.Signature == refund.CalculateSignature(this._signatureService))
                return refund;

            throw new Exception($"Signature error, response signature: {refund.Signature}, calculated signature: {refund.CalculateSignature(this._signatureService)}");
        }
        #endregion

        #region Payment Methods
        public async Task<List<GetPaymentMethodsResponse>> GetPaymentMethods(GetPaymentMethodsRequest request)
        {
            var token = await GetCachedToken();

            return await this.SendAuthenticatedAndReturnResultAsync<GetPaymentMethodsRequest, List<GetPaymentMethodsResponse>>(request, request.Request(this._apiKey.Host), token.Token);
        }
        #endregion

        #region Balance
        public async Task<List<GetBalanceResponse>> GetBalance(GetBalanceRequest request)
        {
            var token = await GetCachedToken();

            return await this.SendAuthenticatedAndReturnResultAsync<GetBalanceRequest, List<GetBalanceResponse>>(request, request.Request(this._apiKey.Host), token.Token);
        }
        #endregion

        #region Callers
        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (var _client = new HttpClient(this._clientHandler, false))
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Add("User-Agent", "Payout .NET library");

                return await _client.SendAsync(request);
            }
        }
        private async Task<TResponse> SendAuthenticatedAndReturnResultAsync<TRequest, TResponse>
            (TRequest requestModel, HttpRequestMessage request, string token)
            where TRequest : BaseRequest
            where TResponse : class
        {
            requestModel.ModelValidation();

            using (var _client = new HttpClient(this._clientHandler, false))
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("User-Agent", "Payout .NET library");

                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    TResponse result = JsonSerializer.Deserialize<TResponse>(body);
                    return result;
                }

                throw new Exception(response.ToString());
            }
        }
        #endregion
    }

}
