namespace invest.Model.NBP
{
    public class Rate
    {
        public int RateId { get; set; }
        public string Currency { get; set; }
        public Currency Code { get; set; }
        public double Mid { get; set; }
    }
}
