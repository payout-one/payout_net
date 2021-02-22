using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Product
    {
        [Required]
        [StringLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
