namespace invest.Steam.Data
{
    public class PriceHistory
    {
        public bool Success { get; set; }
        public string PricePrefix { get; set; }
        public string PriceSuffix { get; set; }
        public List<Price> Prices { get; set; }
    }
}
