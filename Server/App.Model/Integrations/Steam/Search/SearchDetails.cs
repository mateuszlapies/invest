using Raspberry.App.Model.Integrations.Steam.Item;
using System.Text.Json.Serialization;

namespace Raspberry.App.Model.Integrations.Steam.Search
{
    public class SearchDetails
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("hash_name")]
        public string HashName { get; set; }
        [JsonPropertyName("sell_listings")]
        public int SellListings { get; set; }
        [JsonPropertyName("sell_price")]
        public int SellPrice { get; set; }
        [JsonPropertyName("sell_price_text")]
        public string SellPriceText { get; set; }
        [JsonPropertyName("app_icon")]
        public string AppIcon { get; set; }
        [JsonPropertyName("app_name")]
        public string AppName { get; set; }
        [JsonPropertyName("asset_description")]
        public ItemDescription AssetDescription { get; set; }
        [JsonPropertyName("sale_price_text")]
        public string SalePriceText { get; set; }
    }
}
