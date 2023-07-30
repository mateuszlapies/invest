using Raspberry.App.Model.Database;
using Raspberry.App.Model.Services.Chart;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Services.Database
{
    public class ChartService : IChartService
    {
        private readonly IQueryAllByItem<Price> query;

        public ChartService(IQueryAllByItem<Price> query) {
            this.query = query;
        }


        public ChartOptions GetDay(long id)
        {
            var options = Get();
            var prices = query.GetAllByItem(id)
                .Where(p => p.Timestamp > DateTime.UtcNow.AddDays(-1))
                .ToList();
            var groupedPrices = prices.GroupBy(p => p.Timestamp.Hour)
                .Select(p => new { Hour = p.Key, Value = p.Average(x => x.Value) });
            options.XAxis.Data = groupedPrices.Select(g => g.Hour.ToString()).ToList();
            options.Series.First().Data = groupedPrices.Select(g => g.Value).ToList();
            return options;
        }

        public ChartOptions GetMax(long id)
        {
            var options = Get();
            var prices = query.GetAllByItem(id).ToList();
            options.XAxis.Data = prices.Select(p => p.Timestamp.ToShortDateString()).ToList();
            options.Series.First().Data = prices.Select(p => p.Value).ToList();
            return options;
        }

        public ChartOptions GetMonth(long id)
        {
            var options = Get();
            var prices = query.GetAllByItem(id)
                .Where(p => p.Timestamp > DateTime.UtcNow.AddMonths(-1))
                .ToList();
            var groupedPrices = prices.GroupBy(p => p.Timestamp.Month)
                .Select(p => new { Month = p.Key, Value = p.Average(x => x.Value) });
            options.XAxis.Data = groupedPrices.Select(g => g.Month.ToString()).ToList();
            options.Series.First().Data = groupedPrices.Select(g => g.Value).ToList();
            return options;
        }

        public ChartOptions GetYear(long id)
        {
            var options = Get();
            var prices = query.GetAllByItem(id)
                .Where(p => p.Timestamp > DateTime.UtcNow.AddYears(-1))
                .ToList();
            var groupedPrices = prices.GroupBy(p => p.Timestamp.Year)
                .Select(p => new { Year = p.Key, Value = p.Average(x => x.Value) });
            options.XAxis.Data = groupedPrices.Select(g => g.Year.ToString()).ToList();
            options.Series.First().Data = groupedPrices.Select(g => g.Value).ToList();
            return options;
        }

        private ChartOptions Get()
        {
            var options = new ChartOptions()
            {
                XAxis = new ChartAxis()
                {
                    Type = ChartAxisType.Category
                },
                YAxis = new ChartAxis()
                {
                    Type = ChartAxisType.Values
                },
                Series = new List<ChartSeries>()
                {
                    new ChartSeries()
                    {
                        Smooth = true,
                        Type = ChartSeriesType.Line
                    }
                }
            };

            return options;
        }
    }
}
