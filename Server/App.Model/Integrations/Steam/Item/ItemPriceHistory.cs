using System.Text.Json.Serialization;

namespace Raspberry.App.Model.Integrations.Steam.Item
{
    public class ItemPriceHistory
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("price_prefix")]
        public string PricePrefix { get; set; }
        [JsonPropertyName("price_suffix")]
        public string PriceSuffix { get; set; }
        [JsonPropertyName("prices")]
        public List<object[]> Prices { get; set; }
    }
}
