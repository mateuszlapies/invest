using System.Text.Json.Serialization;

namespace Raspberry.App.Model.Integrations.Steam.Item
{
    public class ItemPrice
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("lowest_price")]
        public string LowestPrice { get; set; }
        [JsonPropertyName("volume")]
        public string Volume { get; set; }
        [JsonPropertyName("median_price")]
        public string MedianPrice { get; set; }
    }
}
