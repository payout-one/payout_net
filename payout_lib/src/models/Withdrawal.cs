using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


using Payout.Lib.Base;

namespace Payout.Lib.Models
{
    public class Withdrawal 
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
        [JsonPropertyName("api_key_id")]
        public int ApiKeyId { get; set; }
        [JsonPropertyName("iban")]
        public string Iban { get; set; }
        [JsonPropertyName("bic")]
        public object Bic { get; set; }
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
        [JsonPropertyName("description")]
        public object Description { get; set; }
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }
    }
}
