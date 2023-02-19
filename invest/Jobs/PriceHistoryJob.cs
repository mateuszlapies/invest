using invest.Model;
using invest.Steam;
using invest.Steam.Data;

namespace invest.Jobs
{
    public class PriceHistoryJob
    {
        public static async Task Run(Item item)
        {
            DatabaseContext context = new DatabaseContext();
            SteamService service = new SteamService();
            PriceHistory history = await service.GetPriceHistory(item.Hash);
            history.Prices.ForEach(price =>
            {
                if (!context.History.Where(q => q.Date == price.Date && q.Value == price.Value && q.Volume == q.Volume).Any())
                {
                    History history = new History()
                    {
                        Date = price.Date,
                        Value = price.Value,
                        Volume = price.Volume
                    };
                    item.History.Add(history);
                }
            });
            if (context.ChangeTracker.HasChanges())
            {
                context.SaveChanges();
            }
        }
    }
}
