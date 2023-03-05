using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace invest.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string? Url { get; set; }
        public Currency Currency { get; set; }
        public double BuyPrice { get; set; }
        public int BuyAmount { get; set; }
        [NotMapped]
        public double SellPrice { get; set; }
        [JsonIgnore]
        public List<Daily> Dailies { get; set; }
        public List<Point> Points { get; set; }
        public int Order { get; set; }
    }
}
