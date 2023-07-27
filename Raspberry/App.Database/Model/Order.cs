using Raspberry.App.Database.Enum;

namespace Raspberry.App.Database.Model
{
    public class Order : Base
    {
        public OrderType Type { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
    }
}
