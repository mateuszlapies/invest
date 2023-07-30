using Raspberry.App.Model.Enum;

namespace Raspberry.App.Model.Database
{
    public class Price : Base
    {
        public decimal Value { get; set; }
        public Currency Currency { get; set; }
        public DateTime Timestamp { get; set; }

        public Item Item { get; set; }
    }
}
