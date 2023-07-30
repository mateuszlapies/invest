namespace Raspberry.App.Model.Services.Chart
{
    public class ChartSeries
    {
        public IList<decimal> Data { get; set; }
        public ChartSeriesType Type { get; set; }
        public bool Smooth { get; set; }
    }
}
