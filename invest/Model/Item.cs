namespace invest.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public double BuyPrice { get; set; }
        public int BuyAmount { get; set; }
        public List<History> History { get; set; }
    }
}
