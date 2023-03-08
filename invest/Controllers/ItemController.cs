using Hangfire;
using invest.Data.Request;
using invest.Data.Response;
using invest.Data.Types;
using invest.Jobs;
using invest.Model;
using invest.Model.Steam;
using invest.Services;
using invest.Steam.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace invest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> logger;
        private readonly DatabaseContext dbContext;
        private readonly ItemService service;

        public ItemController(ILogger<ItemController> logger, DatabaseContext dbContext, ItemService service)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.service = service;
        }

        [HttpGet]
        public DataResponse<Search> Search(string query)
        {
            return new DataResponse<Search>().Processed(service.Search(query));
        }

        [HttpGet]
        public DataResponse<UserItem> GetItem(Guid id)
        {
            
            return new DataResponse<UserItem>().Processed(service.Get(id));
        }

        [HttpGet]
        public DataResponse<List<KeyValuePair<Guid, string>>> GetItems(User user)
        {
            return new DataResponse<List<KeyValuePair<Guid, string>>>().Processed(service.GetItems(user));
        }

        [HttpPut]
        public BaseResponse CreateItem(ItemRequest item)
        {
            UserItem userItem = new UserItem()
            {
                BuyPrice = item.BuyPrice,
                BuyAmount = item.BuyAmount,
                Item = new Item()
                {
                    Name = item.Name,
                    Hash = item.Hash,
                }
            };
            

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

        [HttpPost]
        public BaseResponse ReorderItems(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                Item item = dbContext.Items.FirstOrDefault(o => o.ItemId == order[i]);
                if (item != null)
                {
                    item.Order = i;
                } else
                {
                    logger.LogError("Reorder operation failed. Item not found {itemId}", item.ItemId);
                    return new BaseResponse().Failed();
                }
            }
            logger.LogInformation("Reordered items for user");
            dbContext.SaveChanges();
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
