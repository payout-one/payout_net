using System.Net.Http;
using System.Text.Json.Serialization;

namespace Payout.Lib.Base
{
    public abstract class BaseRequest
    {
        public string Host { get; set; }

        [JsonPropertyName("idempotency_key")]
        public string IdempotencyKey { get; set; }

        public abstract HttpRequestMessage Request(string host);
    }
}