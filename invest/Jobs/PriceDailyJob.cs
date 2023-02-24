using invest.Model;
using invest.Steam.Data;
using invest.Steam;
using System.Globalization;

namespace invest.Jobs
{
    public class PriceDailyJob
    {
        private readonly ILogger<PriceHistoryJob> logger;
        private readonly DatabaseContext context;
        private readonly SteamService service;

        public PriceDailyJob(ILogger<PriceHistoryJob> logger, DatabaseContext context, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.context = context;
            service = new SteamService(serviceProvider);
        }

        public void Run(Item i)
        {
            Item item = context.Items.FirstOrDefault(q => q.ItemId == i.ItemId);
            Daily daily = service.GetPrice(item.Hash, item.Currency);
            item.Dailies.Add(daily);
            logger.LogInformation("New daily data point for item {item} has been added | Date: {date} Price: {price} MedianPrice: {medianPrice} Volume: {volume}", item.Name, DateTime.UtcNow, daily.Price, daily.MedianPrice, daily.Volume);
            context.SaveChanges();
        }
    }
}
