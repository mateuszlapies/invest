using Raspberry.App.Model.Database;
using Raspberry.App.Model.Services.Stats;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Services.Database
{
    public class StatsService : IQueryByItem<Stats>
    {
        private readonly IQueryAllByItem<Price> query;

        public StatsService(IQueryAllByItem<Price> query)
        {
            this.query = query;
        }

        public Stats GetByItem(long id)
        {
            var prices = query.GetAllByItem(id);
            var now = prices.First();
            prices = prices.Where(p => p.Id != now.Id);
            var dayAvg = prices.Where(p => p.Timestamp > DateTime.UtcNow.AddDays(-1)).Average(p => p.Value);
            var monthAvg = prices.Where(p => p.Timestamp > DateTime.UtcNow.AddMonths(-1)).Average(p => p.Value);
            var yearAvg = prices.Where(p => p.Timestamp > DateTime.UtcNow.AddYears(-1)).Average(p => p.Value);
            return new Stats()
            {
                Day = (now.Value - dayAvg) / dayAvg * 100,
                Month = (now.Value - monthAvg) / monthAvg * 100,
                Year = (now.Value - yearAvg) / yearAvg * 100
            };
        }
    }
}
