using System.Text.Json.Serialization;


namespace Payout.Lib.Responses
{
    public class DeleteTokenResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
