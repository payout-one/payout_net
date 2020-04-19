using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Payment
    {
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        [JsonPropertyName("failure_reason")]
        public string FailureReason { get; set; }

        [JsonPropertyName("funds")]
        public string Funds { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}