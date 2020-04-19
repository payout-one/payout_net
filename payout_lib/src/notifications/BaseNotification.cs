using System.Text.Json;
using System.Text.Json.Serialization;
using Payout.Lib.Base;
using Payout.Lib.Interfaces;

namespace Payout.Lib.Notifications
{
    public class BaseNotification : BaseSignedResponse
    {
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        public override string CalculateSignature(IPayoutSignature signature)
        {
            return signature.SignNotification(this);
        }

        public override object[] signatureParams()
        {
            return new object[] { this.ExternalId, this.Type, this.Nonce };
        }

        public T TryParse<T>()
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(this.Data));
        }
    }
}