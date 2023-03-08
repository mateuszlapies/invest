namespace invest.Model.Steam
{
    public class Daily
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        public double? Price { get; set; }
        public double MedianPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
