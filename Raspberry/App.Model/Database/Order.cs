using Raspberry.App.Model.Enum;

namespace Raspberry.App.Model.Database
{
    public class Order : Base
    {
        public OrderType Type { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }

        public Item Item { get; set; }
    }
}
