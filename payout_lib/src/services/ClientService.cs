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

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var tokenStatus = JsonSerializer.Deserialize<GetTokenStatusResponse>(body);
                return tokenStatus;
            }

            throw new Exception(response.ToString());
        }
        public async Task<DeleteTokenResponse> DeleteToken(DeleteTokenRequest request)
        {           
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var tokenStatus = JsonSerializer.Deserialize<DeleteTokenResponse>(body);
                return tokenStatus;
            }

            throw new Exception(response.ToString());
        }
        #endregion

        #region Checkout
        public async Task<CheckoutResponse> CreateCheckout(CreateCheckoutRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var checkout = JsonSerializer.Deserialize<CheckoutResponse>(body);

                if (checkout.Signature == checkout.CalculateSignature(this._signatureService))
                    return checkout;

                throw new Exception($"Signature error, response signature: {checkout.Signature}, calculated signature: {checkout.CalculateSignature(this._signatureService)}");
            }

            throw new Exception(response.ToString());
        }

        public async Task<CheckoutResponse> GetCheckout(GetCheckoutRequest request)
        {
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var checkout = JsonSerializer.Deserialize<CheckoutResponse>(body);

                if (checkout.Signature == checkout.CalculateSignature(this._signatureService))
                    return checkout;

                throw new Exception($"Signature error, response signature: {checkout.Signature}, calculated signature: {checkout.CalculateSignature(this._signatureService)}");
            }

            throw new Exception(response.ToString());
        }

        public async Task<List<CheckoutResponse>> GetCheckouts(GetCheckoutsRequest request)
        {
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var checkouts = JsonSerializer.Deserialize<List<CheckoutResponse>>(body);

                if (checkouts.All(a => a.Signature == a.CalculateSignature(this._signatureService)))
                    return checkouts;

                throw new Exception($"Signature error.");
            }

            throw new Exception(response.ToString());
        }

        #endregion

        #region Withdrawals
        public async Task<WithdrawalResponse> CreateWithdrawal(CreateWithdrawalRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var checkout = JsonSerializer.Deserialize<WithdrawalResponse>(body);

                if (checkout.Signature == checkout.CalculateSignature(this._signatureService))
                    return checkout;

                throw new Exception($"Signature error, response signature: {checkout.Signature}, calculated signature: {checkout.CalculateSignature(this._signatureService)}");
            }

            throw new Exception(response.ToString());
        }

        public async Task<WithdrawalResponse> GetWithdrawal(GetWithdrawalRequest request)
        {
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var withdrawal = JsonSerializer.Deserialize<WithdrawalResponse>(body);

                if (withdrawal.Signature == withdrawal.CalculateSignature(this._signatureService))
                    return withdrawal;

                throw new Exception($"Signature error, response signature: {withdrawal.Signature}, calculated signature: {withdrawal.CalculateSignature(this._signatureService)}");
            }

            throw new Exception(response.ToString());
        }

        public async Task<List<WithdrawalResponse>> GetWithdrawals(GetWithdrawalsRequest request)
        {
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var withdrawals = JsonSerializer.Deserialize<List<WithdrawalResponse>>(body);

                if (withdrawals.All(a => a.Signature == a.CalculateSignature(this._signatureService)))
                    return withdrawals;

                throw new Exception($"Signature error.");
            }

            throw new Exception(response.ToString());
        }

        #endregion

        #region Refunds
        public async Task<RefundPaymentResponse> RefundPayment(RefundPaymentRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var refund = JsonSerializer.Deserialize<RefundPaymentResponse>(body);

                if (refund.Signature == refund.CalculateSignature(this._signatureService))
                    return refund;

                throw new Exception($"Signature error, response signature: {refund.Signature}, calculated signature: {refund.CalculateSignature(this._signatureService)}");
            }

            throw new Exception(response.ToString());
        }
        #endregion

        #region Payment Methods
        public async Task<List<GetPaymentMethodsResponse>> GetPaymentMethods(GetPaymentMethodsRequest request)
        {
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var listPaymentMethods = JsonSerializer.Deserialize<List<GetPaymentMethodsResponse>>(body);
                return listPaymentMethods;
            }

            throw new Exception(response.ToString());
        }

        #endregion

        #region Balance

        public async Task<List<GetBalanceResponse>> GetBalance(GetBalanceRequest request)
        {
            var token = await GetCachedToken();

            request.ModelValidation();

            var response = await this.SendAuthenticatedAsync(request.Request(this._apiKey.Host), token.Token);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                var balance = JsonSerializer.Deserialize<List<GetBalanceResponse>>(body);
                return balance;

            }

            throw new Exception(response.ToString());
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

        private async Task<HttpResponseMessage> SendAuthenticatedAsync(HttpRequestMessage request, string token)
        {             
            using (var _client = new HttpClient(this._clientHandler, false))
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("User-Agent", "Payout .NET library");

                return await _client.SendAsync(request);
            }
        }
        #endregion



    }
}