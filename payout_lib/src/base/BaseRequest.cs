using Payout.Lib.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Payout.Lib.Base
{
    public abstract class BaseRequest
    {
        public string Host { get; set; }

        [StringLength(50)]
        [JsonPropertyName("idempotency_key")]
        public string IdempotencyKey { get; set; }

        public abstract HttpRequestMessage Request(string host);

        public virtual void ValidateRequest(IModelValidation validation)
        {
            validation.ValidateRequest(this);
        }
    }
}