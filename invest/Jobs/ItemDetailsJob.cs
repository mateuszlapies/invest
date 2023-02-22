using invest.Model;
using invest.Steam;

namespace invest.Jobs
{
    public class ItemDetailsJob
    {
        private readonly ILogger<ItemDetailsJob> logger;
        private readonly DatabaseContext context;
        private readonly SteamService service;

        public ItemDetailsJob(ILogger<ItemDetailsJob> logger, DatabaseContext context, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.context = context;
            service = new SteamService(serviceProvider);
        }

        public void Run(Item i)
        {
            Item item = context.Items.FirstOrDefault(q => q.ItemId == i.ItemId);
            item.Url = service.GetIconUrl(item.Hash);
            context.SaveChanges();
            logger.LogInformation("Updated icon url of item {item}", item.Name);
        }
    }
}
