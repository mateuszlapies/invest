using invest.Model;
using invest.Steam.Data;
using System.Globalization;
using invest.Model.Steam;
using invest.Services;

namespace invest.Jobs
{
    public class PriceDailyJob
    {
        private readonly ILogger<PriceHistoryJob> logger;
        private readonly DatabaseContext context;
        private readonly SteamService steam;

        public PriceDailyJob(ILogger<PriceHistoryJob> logger, DatabaseContext context, SteamService steam)
        {
            this.logger = logger;
            this.context = context;
            this.steam = steam;
        }

        public void Run(Item i)
        {
            Item item = context.Items.FirstOrDefault(q => q.ItemId == i.ItemId);
            Daily daily = steam.GetPrice(item.Hash, item.Currency);
            daily.Timestamp = DateTime.UtcNow;
            if (item.Dailies == null)
            {
                item.Dailies = new List<Daily>();
            }
            item.Dailies.Add(daily);
            logger.LogInformation("New daily data point for item {item} has been added | Date: {date} Price: {price} MedianPrice: {medianPrice} Volume: {volume}", item.Name, DateTime.UtcNow, daily.Price, daily.MedianPrice, daily.Volume);
            context.SaveChanges();
        }
    }
}
