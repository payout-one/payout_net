using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using Payout.Lib.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payout.Lib.Requests
{
    public class CreateCheckoutRequest : BaseSignedRequest
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
        [JsonPropertyName("metadata")]
        public Dictionary<string, dynamic> Metadata { get; set; }

        [JsonPropertyName("redirect_url")]
        public string RedirectUrl { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Post, $"https://{host}/api/v1/checkouts")
            {
                Content = new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json")
            };
        }

        public override object[] signatureParams() => new object[] {
            this.Amount, this.Currency, this.ExternalId, this.Nonce
        };
    }
}
