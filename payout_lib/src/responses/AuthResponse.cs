using System.Text.Json.Serialization;
using Payout.Lib.Base;

namespace Payout.Lib.Responses
{
    public class AuthResponse : BaseResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("valid_for")]
        public int ValidFor { get; set; }
    }
}