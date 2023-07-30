using System.Text.Json.Serialization;

namespace Raspberry.App.Model.Integrations.Steam.Item
{
    public class ItemDescription
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }
        [JsonPropertyName("classid")]
        public string ClassId { get; set; }
        [JsonPropertyName("instanceid")]
        public string InstanceId { get; set; }
        [JsonPropertyName("background_color")]
        public string BackgroundColor { get; set; }
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }
        [JsonPropertyName("tradable")]
        public int Tradeable { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("name_color")]
        public string NameColor { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("market_name")]
        public string MarketName { get; set; }
        [JsonPropertyName("market_hash_name")]
        public string MarketHashName { get; set; }
        [JsonPropertyName("commodity")]
        public int Commodity { get; set; }
    }
}
