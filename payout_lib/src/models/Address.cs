using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Address
    {
        [Required]
        [StringLength(255)]
        [JsonPropertyName("address_line_1")]
        public string AddressLine1 { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("address_line_2")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }
    }
}