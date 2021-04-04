using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Balance
    {
        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("pending")]
        public int Pending { get; set; }
    }
}