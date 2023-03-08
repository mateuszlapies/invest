using Hangfire;
using invest.Jobs;
using invest.Model;
using invest.Model.Steam;
using invest.Steam.Data;
using Microsoft.EntityFrameworkCore;

namespace invest.Services
{
    public class ItemService
    {
        private readonly ILogger<ItemService> logger;
        private readonly DatabaseContext context;
        private readonly SteamService steam;

        public ItemService(ILogger<ItemService> logger, DatabaseContext context, SteamService steam)
        {
            this.logger = logger;
            this.context = context;
            this.steam = steam;
        }

        public Search Search(string search)
        {
            return steam.SearchItems(search);
        }

        public UserItem Get(Guid id)
        {
            UserItem item = context.UserItems
                .Where(q => q.UserItemId == id)
                .Include(i => i.Item)
                .ThenInclude(i => i.Dailies)
                .FirstOrDefault();

            if (item != null)
            {
                item.SellPrice = item.Item.Dailies
                    .OrderByDescending(o => o.Timestamp)
                    .Select(s => s.Price)
                    .FirstOrDefault() ?? 0;
            }

            return item;
        }

        public List<KeyValuePair<Guid, string>> GetItems(User user)
        {
            return context.UserItems
                .Where(i => i.UserItemId == user.UserId)
                .OrderBy(o => o.Order)
                .Select(s => new KeyValuePair<Guid, string>(s.UserItemId, s.Item.Name))
                .ToList();
        }

        public UserItem Create(UserItem item)
        {
            Item i = context.Items.SingleOrDefault(q => q.Hash == item.Item.Hash);
            if (i == null)
            {
                try
                {
                    item.Item.Url = steam.GetIconUrl(item.Item.Hash);
                } catch (Exception ex)
                {
                    logger.LogError("Item hash provided does not exist. Hash: {hash}", item.Item.Hash);
                    throw new Exception("Item does not exist");
                }
                
                item.Item.Key = GetKey(item.Item.Name);
                context.UserItems.Add(item);
                context.SaveChanges();
                logger.LogInformation("New item has been created {item}", i.Name);

                BackgroundJob.Enqueue<PriceHistoryJob>(x => x.Run(i));
                logger.LogInformation("Scheduled a PriceHistoryJob run for new item {item}", i.Name);
            } else
            {
                item.Item = i;
                context.UserItems.Add(item);
                context.SaveChanges();
            }
            
            return item;
        }

        public UserItem Update(UserItem item)
        {
            UserItem i = context.UserItems.SingleOrDefault(q => q.UserItemId == item.UserItemId);
            i.BuyPrice = item.BuyPrice;
            i.BuyAmount = item.BuyAmount;
            i.Order = item.Order;
            context.SaveChanges();
            return item;
        }

        private string GetKey(string name)
        {
            string[] words = name.Split(' ');
            string key = string.Empty;
            foreach (string word in words)
            {
                key += word.ToUpper().First();
            }
            return key;
        }
    }
}
