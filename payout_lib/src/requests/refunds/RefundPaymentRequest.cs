using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using Payout.Lib.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payout.Lib.Requests
{
    public class RefundPaymentRequest : BaseSignedRequest
    {
        [Required]
        [JsonIgnore]
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [Required]
        [JsonIgnore]
        [StringLength(3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Required]
        [JsonIgnore]
        [StringLength(50)]
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [Required]
        [JsonPropertyName("checkout_id")]
        public long CheckoutId { get; set; }
        [Required]
        [JsonPropertyName("iban")]
        public string Iban { get; set; }
        [Required]
        [JsonPropertyName("statement_descriptor")]
        public string StatementDescriptor { get; set; }




        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Post, $"https://{host}/api/v1/refunds")
            {
                Content = new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json")
            };
        }

        public override object[] signatureParams() => new object[] {
           this.Amount, this.Currency, this.ExternalId, this.Iban, this.Nonce
        };


    }
}
