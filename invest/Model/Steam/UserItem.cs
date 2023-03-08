using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace invest.Model.Steam
{
    public class UserItem
    {
        public Guid UserItemId { get; set; }
        public double BuyPrice { get; set; }
        public int BuyAmount { get; set; }
        [NotMapped]
        public double SellPrice { get; set; }

        public int Order { get; set; }

        public Item Item { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
