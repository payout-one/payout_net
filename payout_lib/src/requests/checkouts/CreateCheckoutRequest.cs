using Payout.Lib.Base;
using Payout.Lib.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payout.Lib.Requests
{
    public class CreateCheckoutRequest : BaseSignedRequest
    {
        [Required]
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [Required]
        [StringLength(3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Required]
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("billing_address")]
        public Address BillingAddress { get; set; }

        [JsonPropertyName("shipping_address")]
        public Address ShippingAddress { get; set; }

        [Required]
        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }

        [Required]
        [StringLength(50)]
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, dynamic> Metadata { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("recurrent_token")]
        public string RecurrentToken { get; set; }

        [JsonPropertyName("redirect_url")]
        public string RedirectUrl { get; set; }

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