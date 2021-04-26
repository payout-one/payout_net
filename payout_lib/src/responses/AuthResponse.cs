using Payout.Lib.Base;
using System.Text.Json.Serialization;

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