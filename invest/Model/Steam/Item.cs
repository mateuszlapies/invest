using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace invest.Model.Steam
{

    [Index(nameof(Key), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Hash), IsUnique = true)]
    public class Item
    {
        public int ItemId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string? Url { get; set; }
        [JsonIgnore]
        public List<Daily> Dailies { get; set; }
        public List<Point> Points { get; set; }
    }
}
