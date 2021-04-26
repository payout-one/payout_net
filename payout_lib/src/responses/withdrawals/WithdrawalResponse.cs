using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using Payout.Lib.Models;
using System.Text.Json.Serialization;

namespace Payout.Lib.Responses
{
    public class WithdrawalResponse : BaseSignedResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        [JsonPropertyName("bic")]
        public object Bic { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        public override string CalculateSignature(IPayoutSignature signature)
        {
            return signature.SignResponse(this);
        }

        public override object[] signatureParams()
            => new object[] { this.Amount, this.Currency, this.ExternalId, this.Iban, this.Nonce };
    }
}