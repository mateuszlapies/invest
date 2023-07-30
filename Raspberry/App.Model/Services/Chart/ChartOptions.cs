namespace Raspberry.App.Model.Services.Chart
{
    public class ChartOptions
    {
        public ChartAxis XAxis { get; set; }
        public ChartAxis YAxis { get; set; }
        public IList<ChartSeries> Series { get; set; }
    }
}
