using Raspberry.App.Database.Enum;

namespace Raspberry.App.Database.Model
{
    public class Price : Base
    {
        public decimal Value { get; set; }
        public Currency Currency { get; set; }
        public DateTime Timestamp { get; set; }

        public Item Item { get; set; }
    }
}
