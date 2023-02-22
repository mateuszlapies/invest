using System.Text.Json.Serialization;

namespace invest.Steam.Data
{
    public class Search
    {
        [JsonPropertyName("results")]
        public List<SearchDetails> Results { get; set; }
    }
}
