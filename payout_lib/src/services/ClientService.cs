using System;
using System.Text.Json;
using System.Threading.Tasks;
using Payout.Lib.Interfaces;
using Payout.Lib.Requests;
using Payout.Lib.Responses;
using Payout.Lib.Models;
using System.Net.Http;
using System.Net.Http.Headers;

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

        public async Task<CheckoutResponse> CreateCheckout(CreateCheckoutRequest request)
        {
            var token = await GetCachedToken();

            request.SignRequest(this._signatureService);

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
    }
}