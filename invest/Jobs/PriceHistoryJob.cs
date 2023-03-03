using invest.Model;
using invest.Steam;
using invest.Steam.Data;
using System.Globalization;

namespace invest.Jobs
{
    public class PriceHistoryJob
    {
        private readonly ILogger<PriceHistoryJob> logger;
        private readonly DatabaseContext context;
        private readonly SteamService steam;
        private readonly CultureInfo provider;

        public PriceHistoryJob(ILogger<PriceHistoryJob> logger, DatabaseContext context, SteamService steam)
        {
            this.logger = logger;
            this.context = context;
            this.steam = steam;
            provider = CultureInfo.GetCultureInfo("en-US");
        }

        public void Run(Item i)
        {
            Item item = context.Items.FirstOrDefault(q => q.ItemId == i.ItemId);
            PriceHistory history = steam.GetPriceHistory(item.Hash, item.Currency);
            history.Prices.ForEach(price =>
            {
                Point p = new Point()
                {
                    Date = DateTime.ParseExact(price[0].ToString(), "MMM dd yyyy HH: z", provider).ToUniversalTime(),
                    Value = double.Parse(price[1].ToString(), provider),
                    Volume = int.Parse(price[2].ToString())
                };

                if (!context.Points.Any(q => q.Item.ItemId == item.ItemId && q.Date == p.Date && q.Value == p.Value && q.Volume == p.Volume))
                {
                    if (item.Points == null)
                    {
                        item.Points = new List<Point>();
                    }
                    item.Points.Add(p);
                    logger.LogInformation("New data point for item {item} has been added | Date: {date} Price: {prefix}{price}{sufix} Volume: {volume}", item.Name, p.Date, history.PricePrefix, p.Value, history.PriceSuffix, p.Volume);
                }
            });
            context.SaveChanges();
        }
    }
}
