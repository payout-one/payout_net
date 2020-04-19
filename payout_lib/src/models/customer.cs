using System.Text.Json.Serialization;

namespace Payout.Lib.Models
{
    public class Customer
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}