namespace Raspberry.App.Model.Database
{
    public class Item : Base
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public List<Price> Prices { get; set; }
    }
}
