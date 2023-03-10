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
            
            service.Create(userItem);

            return new BaseResponse().Succeeded();
        }

        [HttpPost]
        public BaseResponse UpdateItem(ItemRequest item)
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

            service.Update(userItem);

            return new BaseResponse().Succeeded();
        }

        [HttpPost]
        public BaseResponse ReorderItems(Guid[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                UserItem item = dbContext.UserItems.FirstOrDefault(o => o.UserItemId == order[i]);
                if (item != null)
                {
                    item.Order = i;
                } else
                {
                    logger.LogError("Reorder operation failed. Item not found {itemId}", item.UserItemId);
                    return new BaseResponse().Failed();
                }
            }
            dbContext.SaveChanges();
            logger.LogInformation("Reordered items for user");
            return new BaseResponse().Succeeded();
        }

        [HttpDelete]
        public BaseResponse DeleteItem(Guid id)
        {
            UserItem item = dbContext.UserItems.FirstOrDefault(q => q.UserItemId == id);
            if (item == null)
            {
                logger.LogError("Failed to delete item. Id not found {id}", id);
                return new BaseResponse().Failed();
            }
                
            dbContext.UserItems.Remove(item);
            dbContext.SaveChanges();
            logger.LogInformation("Item has been removed for user");

            return new BaseResponse().Succeeded();
        }
    }
}
