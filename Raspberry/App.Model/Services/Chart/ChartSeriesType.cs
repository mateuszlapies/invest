namespace Raspberry.App.Model.Services.Chart
{
    public class ChartSeriesType
    {
        public static ChartSeriesType Line = new("line");

        private readonly string type;

        public ChartSeriesType(string type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return type;
        }
    }
}
