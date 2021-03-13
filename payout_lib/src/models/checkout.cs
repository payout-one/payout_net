using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Checkout
    {

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("billing_address")]
        public Address BillingAddress { get; set; }

        [JsonPropertyName("shipping_address")]
        public Address ShippingAddress { get; set; }

        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("metadata")]
        public object Metadata { get; set; }


        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("payment")]
        public Payment Payment { get; set; }

        [JsonPropertyName("redirect_url")]
        public string RedirectUrl { get; set; }

        [JsonPropertyName("checkout_url")]
        public string CheckoutUrl { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

     
    }
}