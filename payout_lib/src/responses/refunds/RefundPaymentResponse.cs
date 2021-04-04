using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using Payout.Lib.Models;
using System.Text.Json.Serialization;

namespace Payout.Lib.Responses
{
    public class RefundPaymentResponse : BaseSignedResponse
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        public override string CalculateSignature(IPayoutSignature signature)
        {
            return signature.SignResponse(this);
        }

        public override object[] signatureParams()
            => new object[] { this.Amount, this.Currency, this.ExternalId, this.Iban, this.Nonce };
    }
}