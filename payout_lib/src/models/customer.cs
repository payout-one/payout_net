using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Customer
    {
        [Required]
        [StringLength(255)]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [RegularExpression(@"\d{1,3}-\d{3,15}")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}