namespace invest.Model
{
    public class Point
    {
        public int PointId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int Volume { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
