namespace invest.Model.NBP
{
    public class Exchange
    {
        public int ExchangeId { get; set; }
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
