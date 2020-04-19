using System.Text.Json.Serialization;
using Payout.Lib.Interfaces;

namespace Payout.Lib.Base
{
    public abstract class BaseSignedResponse : BaseResponse
    {
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }
        public abstract string CalculateSignature(IPayoutSignature signature);
        public abstract object[] signatureParams();
    }
}