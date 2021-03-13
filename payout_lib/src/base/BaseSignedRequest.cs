using Payout.Lib.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Payout.Lib.Base
{
    public abstract class BaseSignedRequest : BaseRequest
    {
        [Required]
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        [Required]
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        public void SignRequest(IPayoutSignature signature)
        {
            this.Signature = signature.SignRequest(this);
        }

        public abstract object[] signatureParams();
    }
}