using Hangfire;
using invest.Data.Request;
using invest.Data.Response;
using invest.Jobs;
using invest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> logger;
        private readonly DatabaseContext dbContext;

        public ItemController(ILogger<ItemController> logger, DatabaseContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public DataResponse<List<KeyValuePair<int, string>>> GetItems()
        {
            return new DataResponse<List<KeyValuePair<int, string>>>().Processed(dbContext.Items.Select(s => new KeyValuePair<int, string>(s.ItemId, s.Name)).ToList());
        }

        [HttpGet]
        public DataResponse<Item> GetItem(int id)
        {
            return new DataResponse<Item>().Processed(dbContext.Items.Include(i => i.Points).FirstOrDefault(q => q.ItemId == id));
        }

        [HttpPut]
        public BaseResponse CreateItem(ItemRequest item)
        {
            Item i = new Item()
            {
                Name = item.Name,
                Hash = item.Hash,
                BuyPrice = item.BuyPrice,
                BuyAmount = item.BuyAmount
            };
            dbContext.Items.Add(i);
            dbContext.SaveChanges();
            logger.LogInformation("New item has been created {item}", i.Name);

            BackgroundJob.Enqueue<ItemDetailsJob>(x => x.Run(i));
            logger.LogInformation("Scheduled a ItemDetailsJob run for new item {item}", i.Name);

            BackgroundJob.Enqueue<PriceHistoryJob>(x => x.Run(i));
            logger.LogInformation("Scheduled a PriceHistoryJob run for new item {item}", i.Name);

            return new BaseResponse().Succeeded();
        }

        [HttpPost]
        public BaseResponse UpdateItem(ItemRequest item)
        {
            Item i = dbContext.Items.FirstOrDefault(q => q.ItemId == item.ItemId);

            if (i == null)
            {
                logger.LogError("Failed to update item. Id not found {id}", item.ItemId);
                return new BaseResponse().Failed();
            }

            i.Name = item.Name;
            i.Hash = item.Hash;
            i.BuyPrice = item.BuyPrice;
            i.BuyAmount = item.BuyAmount;

            dbContext.SaveChanges();
            logger.LogInformation("Item has been updated {item}", i.Name);

            BackgroundJob.Enqueue<ItemDetailsJob>(x => x.Run(i));
            logger.LogInformation("Scheduled a ItemDetailsJob run for new item {item}", i.Name);

            BackgroundJob.Enqueue<PriceHistoryJob>(x => x.Run(i));
            logger.LogInformation("Scheduled a PriceHistoryJob run for new item {item}", i.Name);

            return new BaseResponse().Succeeded();
        }

        [HttpDelete]
        public BaseResponse DeleteItem(int id)
        {
            Item item = dbContext.Items.FirstOrDefault(q => q.ItemId == id);
            if (item == null)
            {
                logger.LogError("Failed to delete item. Id not found {id}", id);
                return new BaseResponse().Failed();
            }
                
            dbContext.Items.Remove(item);
            dbContext.SaveChanges();
            logger.LogInformation("Item has been removed {item}", item.Name);

            return new BaseResponse().Succeeded();
        }
    }
}
