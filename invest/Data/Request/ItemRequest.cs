namespace invest.Data.Request
{
    public class ItemRequest
    {
        public int? ItemId { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string? Url { get; set; }
        public double BuyPrice { get; set; }
        public int BuyAmount { get; set; }
    }
}
