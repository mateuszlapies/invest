using System.Text.Json.Serialization;

namespace Raspberry.App.Model.Integrations.Steam.Search
{
    public class Search
    {
        [JsonPropertyName("results")]
        public List<SearchDetails> Results { get; set; }
    }
}
