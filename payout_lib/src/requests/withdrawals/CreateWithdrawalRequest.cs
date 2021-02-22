using Payout.Lib.Base;
using Payout.Lib.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payout.Lib.Requests
{
    public class CreateWithdrawalRequest : BaseSignedRequest
    {
        [Required]
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [Required]
        [StringLength(3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Required]
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }


        [Required]
        [StringLength(50)]
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [Required]
        [JsonPropertyName("iban")]
        public string Iban { get; set; }




        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage(HttpMethod.Post, $"https://{host}/api/v1/withdrawals")
            {
                Content = new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json")
            };
        }

        public override object[] signatureParams() => new object[] {
            this.Amount, this.Currency, this.ExternalId, this.Iban, this.Nonce
        };
    }
}
