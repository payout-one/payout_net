using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    class Authentication
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
    }
}