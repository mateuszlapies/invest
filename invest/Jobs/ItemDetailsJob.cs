using invest.Model;
using invest.Model.Steam;
using invest.Services;

namespace invest.Jobs
{
    public class ItemDetailsJob
    {
        private readonly ILogger<ItemDetailsJob> logger;
        private readonly DatabaseContext context;
        private readonly SteamService steam;

        public ItemDetailsJob(ILogger<ItemDetailsJob> logger, DatabaseContext context, SteamService steam)
        {
            this.logger = logger;
            this.context = context;
            this.steam = steam;
        }

        public void Run(Item i)
        {
            Item item = context.Items.FirstOrDefault(q => q.ItemId == i.ItemId);
            item.Url = steam.GetIconUrl(item.Hash);
            context.SaveChanges();
            logger.LogInformation("Updated icon url of item {item}", item.Name);
        }
    }
}
