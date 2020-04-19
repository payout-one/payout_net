using System.Text.Json.Serialization;
using Payout.Lib.Interfaces;

namespace Payout.Lib.Base
{
    public abstract class BaseSignedRequest : BaseRequest
    {

        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        public void SignRequest(IPayoutSignature signature)
        {
            this.Signature = signature.SignRequest(this);
        }

        public abstract object[] signatureParams();
    }
}