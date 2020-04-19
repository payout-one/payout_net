using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class ApiKey
    {
        [JsonPropertyName("client_id")]
        public string Key { get; set; }

        [JsonPropertyName("client_secret")]
        public string Secret { get; set; }

        public string Host { get; set; }
    }
}