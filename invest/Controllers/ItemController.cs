﻿using Hangfire;
using invest.Data.Request;
using invest.Data.Response;
using invest.Data.Types;
using invest.Jobs;
using invest.Model;
using invest.Steam;
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
        private readonly SteamService steam;

        public ItemController(ILogger<ItemController> logger, DatabaseContext dbContext, SteamService steam)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.steam = steam;
        }

        [HttpGet]
        public DataResponse<Search> Search(string query)
        {
            return new DataResponse<Search>().Processed(steam.SearchItems(query));
        }

        [HttpGet]
        public DataResponse<Item> GetItem(int id)
        {
            return new DataResponse<Item>().Processed(dbContext.Items.FirstOrDefault(q => q.ItemId == id));
        }

        [HttpGet]
        public DataResponse<List<KeyValuePair<int, string>>> GetItems()
        {
            return new DataResponse<List<KeyValuePair<int, string>>>().Processed(dbContext.Items.OrderBy(o => o.Order).Select(s => new KeyValuePair<int, string>(s.ItemId, s.Name)).ToList());
        }

        [HttpGet]
        public DataResponse<Item> GetChart(int id, ChartType type)
        {
            Item item = dbContext.Items.FirstOrDefault(q => q.ItemId == id);
            switch (type) {
                case ChartType.Overall: 
                {
                    item.Points = dbContext.Points
                            .Where(q => q.ItemId == id)
                            .GroupBy(g => g.Date.Date)
                            .Select(s => new Point() { Date = s.Key, Value = Math.Round(s.Average(sel => sel.Value), 2), Volume = s.Sum(sel => sel.Volume) })
                            .OrderBy(o => o.Date)
                            .ToList();
                    break;
                }

                case ChartType.Year:
                {
                    item.Points = dbContext.Points
                            .Where(q => q.ItemId == id && q.Date > DateTime.UtcNow.AddYears(-1))
                            .GroupBy(g => g.Date.Date)
                            .Select(s => new Point() { Date = s.Key, Value = Math.Round(s.Average(sel => sel.Value), 2), Volume = s.Sum(sel => sel.Volume) })
                            .OrderBy(o => o.Date)
                            .ToList();
                    break;
                }

                case ChartType.Month:
                {
                    item.Points = dbContext.Points
                            .Where(q => q.ItemId == id && q.Date > DateTime.UtcNow.AddMonths(-1))
                            .GroupBy(g => g.Date.Date)
                            .Select(s => new Point() { Date = s.Key, Value = Math.Round(s.Average(sel => sel.Value), 2), Volume = s.Sum(sel => sel.Volume) })
                            .OrderBy(o => o.Date)
                            .ToList();
                    break;
                }

                case ChartType.Week:
                {
                    item.Points = dbContext.Points
                            .Where(q => q.ItemId == id && q.Date > DateTime.UtcNow.AddDays(-7))
                            .OrderBy(o => o.Date)
                            .ToList();
                    break;
                }

                case ChartType.Day:
                {
                    item.Points = dbContext.Points
                            .Where(q => q.ItemId == id && q.Date > DateTime.UtcNow.AddDays(-1))
                            .OrderBy(o => o.Date)
                            .ToList();
                    break;
                }

                default:
                {
                    break;
                }
            }
            return new DataResponse<Item>().Processed(item);
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
