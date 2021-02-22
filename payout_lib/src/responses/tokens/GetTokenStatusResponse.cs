using System.Text.Json.Serialization;


namespace Payout.Lib.Responses
{
    public class GetTokenStatusResponse
    {
        [JsonPropertyName("cardExpirationYear")]
        public string CardExpirationYear { get; set; }

        [JsonPropertyName("cardExpirationMonth")]
        public string CardExpirationMonth { get; set; }

        [JsonPropertyName("cardNumberMasked")]
        public string CardNumberMasked { get; set; }

        [JsonPropertyName("cardBrand")]
        public string CardBrand { get; set; }

        [JsonPropertyName("value")]
        public string value { get; set; }

        [JsonPropertyName("brandImageUrl")]
        public string BrandImageUrl { get; set; }

        [JsonPropertyName("preferred")]
        public bool Preferred { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

    }
}
