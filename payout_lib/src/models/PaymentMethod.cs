using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class PaymentMethod
    {
        [JsonPropertyName("fixed_fee")]
        public int FixedFee { get; set; }

        [JsonPropertyName("identificator")]
        public string Identificator { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("percentual_fee")]
        public decimal PercentualFee { get; set; }
    }
}