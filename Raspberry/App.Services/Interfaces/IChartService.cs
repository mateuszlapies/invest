using Raspberry.App.Model.Services.Chart;

namespace Raspberry.App.Services.Interfaces
{
    public interface IChartService
    {
        public ChartOptions GetMax(long id);
        public ChartOptions GetYear(long id);
        public ChartOptions GetMonth(long id);
        public ChartOptions GetDay(long id);
    }
}
