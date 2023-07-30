namespace Raspberry.App.Model.Services.Chart
{
    public class ChartAxisType
    {
        public static ChartAxisType Category = new("category");
        public static ChartAxisType Values = new("values");

        private readonly string type;

        public ChartAxisType(string type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return type;
        }
    }
}
