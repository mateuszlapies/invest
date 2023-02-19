using Newtonsoft.Json;

namespace invest.Steam.Data
{
    [JsonConverter(typeof(PriceConverter))]
    public class Price
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int Volume { get; set; }
    }
}
