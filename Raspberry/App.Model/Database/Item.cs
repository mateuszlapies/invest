namespace Raspberry.App.Model.Database
{
    public class Item : Base
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public bool IsHidden { get; set; }

        public List<Price> Prices { get; set; }
        public List<Order> Orders { get; set; }
    }
}
